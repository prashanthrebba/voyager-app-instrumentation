using Microsoft.AspNetCore.Mvc;
using Voyager.Api.Services;
using Voyager.Api.Views;

namespace Voyager.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
    {
        _logger = logger;
        _recipeService = recipeService;
    }

    [HttpGet(Name = "GetRandomRecipe")]
    public async Task<ActionResult<Joke>> GetRandomRecipeAsync()
    {
        try
        {
            var startTime = DateTime.Now;
            _logger.LogInformation("Fetching random recipe at timeStamp : {@TimeStamp}", startTime);
            var recipe = await _recipeService.GetRandomRecipeAsync();
            var endTime = DateTime.Now;
            _logger.LogInformation("Successfully fetched the random recipe at timeStamp : {@TimeStamp} and TimeElasped:{@TimeElapsed}", endTime, endTime - startTime);
            return Ok(recipe);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching random recipe");
            return StatusCode(500, ex.Message);
        }
    }
}
