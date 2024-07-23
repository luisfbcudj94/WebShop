using WebShopAPI.Core.Entities;

namespace WebShopAPI.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid categoryId);
    }
}
