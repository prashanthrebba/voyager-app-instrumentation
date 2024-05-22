using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Voyager.Api.Services;

namespace Voyager.Api.ServicesImpl;

public class RecipeService : IRecipeService
{
    private readonly ILogger<RecipeService> _logger;

    public RecipeService(
        ILogger<RecipeService> logger
    )
    {
        _logger = logger;
    }
    public Task<IEnumerable<Guid>> GetRecipeAsync()
    {
        throw new NotImplementedException();
    }
}

