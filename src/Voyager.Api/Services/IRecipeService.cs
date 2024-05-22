using Voyager.Api.Views;
namespace Voyager.Api.Services;
public interface IRecipeService
{
    public Task<IEnumerable<Guid>> GetRecipeAsync();
}
