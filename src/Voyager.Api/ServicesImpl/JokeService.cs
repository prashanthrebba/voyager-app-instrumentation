using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Voyager.Api.Services;
using Voyager.Api.Views;

namespace Voyager.Api.ServicesImpl;

public class JokeService : IJokeService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<JokeService> _logger;
    private const string endpoint = "https://api.sampleapis.com/jokes/goodJokes";

    public JokeService(ILogger<JokeService> logger
, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<Joke> GetRandomJokeAsync()
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var jokes = await response.Content.ReadFromJsonAsync<List<Joke>>();
        return jokes[new Random().Next(jokes.Count)];
    }
}

