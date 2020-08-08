using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureProject.Domain.Models;
using ArchitectureProject.Logic.WeatherForecasts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArchitectureProject.Api.Controllers
{
    public class WeatherForecastController : ApiController
    {
        public WeatherForecastController(IMediator mediator) : base(mediator)
        {}

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await this.Mediator.Send(new GetWeatherForecastQuery());
        }

    }
}
