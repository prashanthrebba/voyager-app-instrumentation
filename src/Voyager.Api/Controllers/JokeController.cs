using Microsoft.AspNetCore.Mvc;
using Voyager.Api.Services;
using Voyager.Api.Views;

namespace Voyager.Api.Controllers;

[ApiController]
[Route("[controller]")]
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
    // [Route("api/jokes/random")]
    public async Task<ActionResult<Joke>> GetRandomJoke()
    {
        try
        {
            var joke = await _jokeService.GetRandomJokeAsync();
            return Ok(joke);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
