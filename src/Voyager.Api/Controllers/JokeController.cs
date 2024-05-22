using Microsoft.AspNetCore.Mvc;
using Voyager.Api.Services;
using Voyager.Api.Views;

namespace Voyager.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class JokeController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<JokeController> _logger;
    private readonly IJokeService _jokeService;

    public JokeController(ILogger<JokeController> logger, IJokeService jokeService)
    {
        _logger = logger;
        _jokeService = jokeService;
    }

    [HttpGet(Name = "GetRandomJoke")]
    public async Task<ActionResult<Joke>> GetRandomJokeAsync()
    {
        try
        {
            var startTime = DateTime.Now;
            _logger.LogInformation("Fetching random joke at timeStamp : {@TimeStamp}", startTime);
            var joke = await _jokeService.GetRandomJokeAsync();
            var endTime = DateTime.Now;
            _logger.LogInformation("Successfully fetched the random joke at timeStamp : {@TimeStamp} and TimeElasped:{@TimeElapsed}", endTime, endTime - startTime);
            return Ok(joke);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching random joke");
            return StatusCode(500, ex.Message);
        }
    }
}
