using Microsoft.AspNetCore.Mvc;

namespace aspnet_streaming_json.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IAsyncEnumerable<WeatherForecast>> Get()
        {
            async IAsyncEnumerable<WeatherForecast> StreamWeatherForecastAsync()
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000);
                    var item = new WeatherForecast
                    {
                        Id = i,
                        Date = DateTime.Now.AddDays(i),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    };
                    
                    yield return item;
                }
            }

            return StreamWeatherForecastAsync();
        }
    }
}