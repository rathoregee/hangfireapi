using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace g2v.core.clinetsync.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBackgroundJobClient _jobClient;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBackgroundJobClient jobClient)
        {
            _logger = logger;
            _jobClient = jobClient;
        }

        [HttpGet(Name = "fire")]
        public IEnumerable<WeatherForecast> Get()
        {
            _jobClient.Enqueue(() => Console.WriteLine("Fire-and-forget!"));

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}