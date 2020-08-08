using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitectureProject.Api.Controllers
{
    [ApiController]
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
