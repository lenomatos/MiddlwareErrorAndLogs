using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiddleWareIIsChange.Service;
using System;
using System.Linq;

namespace MiddleWareIIsChange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISampleService _sampleClass;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISampleService sampleClass)
        {
            _logger = logger;
            _sampleClass = sampleClass;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            _logger.LogInformation("Start GetAsync");

            // Force asynchronous method to run synchronously
            _sampleClass.ProcessItemsAsync().GetAwaiter().GetResult();

            var rng = new Random();
            var response = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();

            _logger.LogInformation($"End GetAsync: {string.Join(",", response)}");

            return Ok(response);
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public IActionResult Post()
        {
            _logger.LogInformation("Start GetAsync");

            // Force asynchronous method to run synchronously
            _sampleClass.ProcessItemsAsync().GetAwaiter().GetResult();

            var rng = new Random();
            var response = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();

            _logger.LogInformation($"End GetAsync: {string.Join(",", response)}");

            return Ok(response);
        }


    }
}