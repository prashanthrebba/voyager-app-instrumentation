using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Voyager.Api.Services;

namespace Voyager.Api.ServicesImpl;

public class GameService : IGameService
{
    private readonly ILogger<GameService> _logger;

    public GameService(
        ILogger<GameService> logger
    )
    {
        _logger = logger;
    }

    public Task<IEnumerable<Guid>> GetGameAsync()
    {
        throw new NotImplementedException();
    }
}

