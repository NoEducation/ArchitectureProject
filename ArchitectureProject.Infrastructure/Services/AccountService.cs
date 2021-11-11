using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ArchitectureProject.Domain.Dto;
using ArchitectureProject.Domain.Errors;
using ArchitectureProject.Domain.Models;
using ArchitectureProject.Domain.Static;
using ArchitectureProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;


namespace ArchitectureProject.Infrastructure.Services
{
    // TODO.DA This service will be separated into command (Operation Result), Queries
    public class AccountService : IAccountService
    {
        private readonly ArchitectureProjectDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IConfiguration _configuration;
        private string _errorMessage => "Login or password incorrect";

        public AccountService(ArchitectureProjectDbContext context,
            ITokenService tokenService,
            IPasswordHasherService passwordHasherService,
            IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasherService = passwordHasherService;
            _configuration = configuration;
        }    

        public async Task<LoggedTokenDto> Login(LoginUserDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user is null)
            {
                throw new AuthorizationArchitectureException(_errorMessage);
            }

            var hash = _passwordHasherService.GenerateHash(model.Password, GetSalt(user.AddedDate));

            if (hash != user.Password)
            {
                throw new AuthorizationArchitectureException(_errorMessage);
            }

            var refreshTokenKey = _tokenService.GenerateRefreshToken();
            var refreshToken = CreateRefreshToken(user, refreshTokenKey);
            var accessToken = _tokenService.GenerateAccessToken(new List<Claim>() {new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())});

            if (user.RefreshTokens?.Any() == true)
            {
                var lastAvailableToken = user.RefreshTokens.SingleOrDefault(x => x.IsActive);
                if (lastAvailableToken != null)
                {
                    lastAvailableToken.Revoked = DateTime.Now;
                    lastAvailableToken.ReplacedByToken = refreshTokenKey;
                }
            }

            user.RefreshTokens.Add(refreshToken);
            await this._context.SaveChangesAsync();
            return new LoggedTokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenKey
            };
        }

        public async Task Register(RegisterUserDto model)
        {
            var emailIsInUse = await this._context.Users.AnyAsync(x => x.Email == model.Email);
            if(emailIsInUse)
                throw new ValidationArchitectureException(nameof(User.Name),"Email is in use");

            var nameIsInUse = await this._context.Users.AnyAsync(x => x.Name == model.Name);
            if(nameIsInUse)
                throw  new ValidationArchitectureException(nameof(User.Name), "Name is in use");

            var user = new User()
            {
                AddedDate = DateTime.Now,
                Email = model.Email,
                Name = model.Name,
                RoleId = Roles.NormalUser,
            };

            var passwordHash = _passwordHasherService.GenerateHash(model.Password, GetSalt(user.AddedDate));

            user.Password = passwordHash;

            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();
        }

        public async Task<string> GetAccessToken(string refreshTokenKey, int userId)
        {
            var refreshTokenValid = (await this._context.Users.SingleOrDefaultAsync(u => u.UserId == userId))
                .RefreshTokens.Any(x => x.Token == refreshTokenKey && x.IsActive);

            if(!refreshTokenValid)
                throw new AuthorizationArchitectureException("Refresh token invalid");

            var accessToken = _tokenService.GenerateAccessToken(new List<Claim>()
                { new Claim(ClaimTypes.NameIdentifier, userId.ToString())});

            return accessToken;
        }

        public async Task RevokeRefreshToken(string refreshTokenKey, int userId)
        {
            var refreshTokenValid = (await this._context.Users.SingleOrDefaultAsync(u => u.UserId == userId))
                .RefreshTokens.Any(x => x.Token == refreshTokenKey && x.IsActive);

            if (!refreshTokenValid)
                throw new AuthorizationArchitectureException("Refresh token invalid");

            var refreshToken = (await this._context.Users.SingleOrDefaultAsync(u => u.UserId == userId))
                .RefreshTokens.SingleOrDefault(x => x.IsActive);

            refreshToken.Expires = DateTime.Now;
            refreshToken.Revoked = DateTime.Now;

            await this._context.SaveChangesAsync();
        }

        private RefreshToken CreateRefreshToken(User user, string refreshTokenKey)
        {
            var refreshTokenValidTimeDays = _configuration.GetValue<int>("Token:RefreshTokenTimeValid");

            var entity = new RefreshToken()
            {
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(refreshTokenValidTimeDays),
                Token = refreshTokenKey,
                UserId = user.UserId,
            };

            return entity;
        }

        public string GetSalt(DateTime createdDate)
        {
            var salt = _configuration.GetValue<string>("PasswordHash:Salt");
            return string.Format(salt, createdDate.ToLocalTime());
        }


    }
}
