using Newtonsoft.Json;
using Voyager.Api.Services;
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
        using (_httpClient)
        {
            try
            {
                _logger.LogInformation("Fetching random game from API: {Endpoint}", endpoint);


                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var games = JsonConvert.DeserializeObject<List<Game>>(responseBody) ?? new List<Game>();
                return games[Random.Shared.Next(games.Count)];

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error getting games from API: {Message}", ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error getting games: {Message}", ex.Message);
                throw;
            }
        }
    }
}

