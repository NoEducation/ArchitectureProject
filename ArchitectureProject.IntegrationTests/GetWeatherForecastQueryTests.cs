using System.Linq;
using System.Threading.Tasks;
using ArchitectureProject.Logic.WeatherForecasts.Queries;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ArchitectureProject.IntegrationTests
{
    public class GetWeatherForecastQueryTests : IntegrationTestBase
    {
        [Test]
        public async Task ValidRequest_OneItemIsReturned()
        {
            var query = new GetWeatherForecastQuery();

            var result =await Mediator.Send(query);

            Assert.AreEqual(5, result.Count());
        }
    }
}
