using Microsoft.AspNetCore.Mvc;
using ReactApp.Server.Controllers.V2;

namespace ReactApp.Server.Controllers.V1
{
    //Install-Package Microsoft.AspNetCore.Mvc.Versioning -Version 5.0.0


    [ApiController]
    [Route("api/v{version:apiVersion}/WeatherForecast")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        [MapToApiVersion("1.0")]
        public IEnumerable<WeatherForecast> Get()
        {
            return GenerateWeatherForecasts();
        }

        [HttpGet("GetWeatherForecast")]
        [MapToApiVersion("2.0")]
        public IEnumerable<WeatherForecast> GetV2()
        {
            return GenerateWeatherForecasts();
        }
        private IEnumerable<WeatherForecast> GenerateWeatherForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
        }
    }

}
