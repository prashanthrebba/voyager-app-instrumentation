using Voyager.Api.Services;
using Voyager.Api.Views;

namespace Voyager.Api.ServicesImpl;

public class JokeService : IJokeService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<JokeService> _logger;
    private const string endpoint = "https://api.sampleapis.com/jokes/goodJokes";

    public JokeService(ILogger<JokeService> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }

    public async Task<Joke> GetRandomJokeAsync()
    {

        using (_httpClient)
        {
            try
            {
                _logger.LogInformation("Fetching random joke from API: {Endpoint}", endpoint);

                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var jokes = await response.Content.ReadFromJsonAsync<List<Joke>>() ?? new List<Joke>();
                return jokes[Random.Shared.Next(jokes.Count)];
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

