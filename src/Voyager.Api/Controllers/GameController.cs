using Microsoft.AspNetCore.Mvc;
using Voyager.Api.Services;
using Voyager.Api.Views;

namespace Voyager.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameService _gameService;

    public GameController(IGameService gameService, ILogger<GameController> logger)
    {
        _logger = logger;
        _gameService = gameService;
    }

    [HttpGet(Name = "GetRandomGame")]
    public async Task<ActionResult<Game>> GetRandomGameAsync()
    {
        try
        {
            var startTime = DateTime.Now;
            _logger.LogInformation("Fetching random game at timeStamp : {@TimeStamp}", startTime);
            var game = await _gameService.GetRandomGameAsync();
            var endTime = DateTime.Now;
            _logger.LogInformation("Successfully fetched the random game at timeStamp : {@TimeStamp} and TimeElasped:{@TimeElapsed}", endTime, endTime - startTime);
            return Ok(game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching random game");
            return StatusCode(500, ex.Message);
        }
    }
}
