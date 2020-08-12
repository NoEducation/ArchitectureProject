using System.Threading.Tasks;
using ArchitectureProject.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {}

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoggedTokenDto>> Login(LoginUserDto model)
        {

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            return Ok();
        }

        //TODO.DA : dodatkowe endpoint do
        //revoke-token
        //refresh-token
        

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<LoggedTokenDto>> RefreshToken(string refreshToken)
        {

            return Ok();
        }
    }
}