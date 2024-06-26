using System.Diagnostics;
using OpenTelemetry.Trace;
using Voyager.Api.Extensions;
using Voyager.Api.Services;
using Voyager.Api.Utils;
using Voyager.Api.Views;


namespace Voyager.Api.ServicesImpl;

public class RecipeService : IRecipeService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RecipeService> _logger;
    private const string endpoint = "https://api.sampleapis.com/recipes/recipes";

    public RecipeService(ILogger<RecipeService> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }

    public async Task<Recipe> GetRandomRecipeAsync()
    {
        var previousActivity = Activity.Current;
        Activity.Current = null;
        using var activity = Telemetry.AppActivitySource?.StartActivity("Api.Request.Get.Random.Recipe");

        using (_httpClient)
        {
            try
            {
                _logger.LogInformation("Fetching random recipe from API: {Endpoint}", endpoint);

                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>() ?? new List<Recipe>();
                var recipe = recipes[Random.Shared.Next(recipes.Count)];
                activity?.SetTag("recipe.result.info", recipe);
                Activity.Current = previousActivity;
                return recipe;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error getting recipes from API: {Message}", ex.Message);
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                activity?.RecordErrorException(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error getting recipes: {Message}", ex.Message);
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                activity?.RecordErrorException(ex);
                throw;
            }
        }
    }
}

