using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using ArchitectureProject.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ArchitectureProject.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var section = _configuration.GetSection("Token");

            var secrete = section.GetValue<string>("Secrete");
            var accessTokenTimeValid = section.GetValue<string>("AccessTokenTimeValid");

            var jwtHandler = new JwtSecurityTokenHandler()

        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
