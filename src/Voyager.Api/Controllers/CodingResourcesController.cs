using Microsoft.AspNetCore.Mvc;
using Voyager.Api.Views;

namespace Voyager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CodingResourcesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<CodingResourcesController> _logger;

    public CodingResourcesController(ILogger<CodingResourcesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWedatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
