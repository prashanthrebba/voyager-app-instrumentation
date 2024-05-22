using Voyager.Api.Views;

namespace Voyager.Api.Services;
public interface ICodingResourceService
{
    public Task<IEnumerable<Guid>> GetCodingResourceAsync();
}
