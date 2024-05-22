using Voyager.Api.Views;

namespace Voyager.Api.Services;
public interface IGameService
{
    public Task<Game> GetRandomGameAsync();
}
