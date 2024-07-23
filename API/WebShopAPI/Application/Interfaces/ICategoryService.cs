using WebShopAPI.Application.DTOs;

namespace WebShopAPI.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId);
    }
}
