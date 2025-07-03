using DineMasterApi.DTO;

namespace DineMasterApi.Repo
{
    public interface IRecipeitem
    {
        Task<RecipeItemDto> AddRecipeitemAsync(RecipeItemDto item);
        Task<IEnumerable<RecipeItemDto>> GetAllRecipeitemsAsync();
        Task<bool> DeleteRecipeItemAsync(int id);
        Task<RecipeItemDto> GetRecipeItemByIdAsync(int id);
        Task<RecipeItemDto> UpdateRecipeItemAsync(int id, RecipeItemDto item);

    }
}
