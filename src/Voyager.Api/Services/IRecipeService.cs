using Voyager.Api.Views;
namespace Voyager.Api.Services;
public interface IRecipeService
{
    public Task<Recipe> GetRandomRecipeAsync();
}
