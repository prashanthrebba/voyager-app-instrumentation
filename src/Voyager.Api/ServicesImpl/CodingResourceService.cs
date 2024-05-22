using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Voyager.Api.Services;

namespace Voyager.Api.ServicesImpl;

public class CodingResourceService : ICodingResourceService
{
    private readonly ILogger<CodingResourceService> _logger;

    public CodingResourceService(
        ILogger<CodingResourceService> logger
    )
    {
        _logger = logger;
    }

    public Task<IEnumerable<Guid>> GetCodingResourceAsync()
    {
        throw new NotImplementedException();
    }
}

