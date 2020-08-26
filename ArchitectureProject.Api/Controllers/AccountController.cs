using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ArchitectureProject.Domain.Dto;
using ArchitectureProject.Domain.Errors;
using ArchitectureProject.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ArchitectureProject.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IMediator mediator,
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor) : base(mediator)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Login")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody]LoginUserDto model)
        {
            LoggedTokenDto result = new LoggedTokenDto();

            try
            {
                result = await _accountService.Login(model);
            }
            catch (AuthorizationArchitectureException ex)
            {
                return BadRequest(ex.Errors);
            }

            SetTokenCookie(result.RefreshToken);

            return Ok(result.AccessToken);

        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            try
            {
                await this._accountService.Register(model);
            }
            catch (ValidationArchitectureException exception)
            {
                return BadRequest(exception.Errors.ToString());
            }

            return Ok();
        }


        [HttpGet("RefreshToken")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RefreshToken(int userId)
        {
            string accessToken = null;
            var refreshToken = Request.Cookies["refreshToken"];

            try
            {
                accessToken = await this._accountService.GetAccessToken(refreshToken, userId);
            }
            catch (AuthorizationArchitectureException)
            {
                return Unauthorized();
            }
            
            return Ok(accessToken);
        }

        [HttpPost("RevokeRefreshToken")]
        public async Task<ActionResult<string>> RevokeRefreshToken(string refreshToken)
        {
            try
            {
                await this._accountService.RevokeRefreshToken(refreshToken,
                    Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }
            catch (AuthorizationArchitectureException)
            {   
                return BadRequest("Invalid token");
            }

            return Ok();
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7) // TODO.DA zwraca cały obiekt i ustwaic tu wartość
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}