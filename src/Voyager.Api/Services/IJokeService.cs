using Voyager.Api.Views;

namespace Voyager.Api.Services;
public interface IJokeService
{
    public Task<Joke> GetRandomJokeAsync();
}
