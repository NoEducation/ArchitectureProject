using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureProject.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected IMediator Mediator;
        protected ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }  

    }
}
