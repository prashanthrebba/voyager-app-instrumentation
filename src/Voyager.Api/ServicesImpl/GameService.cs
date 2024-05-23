using System.Diagnostics;
using Newtonsoft.Json;
using OpenTelemetry.Trace;
using Voyager.Api.Extensions;
using Voyager.Api.Services;
using Voyager.Api.Utils;
using Voyager.Api.Views;

namespace Voyager.Api.ServicesImpl;

public class GameService : IGameService
{
    private readonly ILogger<GameService> _logger;
    private readonly HttpClient _httpClient;
    private const string endpoint = "https://api.sampleapis.com/xbox/games";

    public GameService(
        ILogger<GameService> logger
    )
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }

    public async Task<Game> GetRandomGameAsync()
    {
        var previousActivity = Activity.Current;
        Activity.Current = null;
        using var activity = Telemetry.AppActivitySource?.StartActivity("Api.Request.Get.Random.Game");
        using (_httpClient)
        {
            try
            {
                _logger.LogInformation("Fetching random game from API: {Endpoint}", endpoint);


                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<List<Game>>(responseBody) ?? new List<Game>();
                var game = games[Random.Shared.Next(games.Count)];
                activity?.SetTag("game.result.info", game);
                Activity.Current = previousActivity;
                return game;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error getting games from API: {Message}", ex.Message);
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                activity?.RecordErrorException(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error getting games: {Message}", ex.Message);
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                activity?.RecordErrorException(ex);
                throw;
            }
        }
    }
}

