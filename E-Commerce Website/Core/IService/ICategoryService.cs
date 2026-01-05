using E_Commerce_Website.Core.DTO;

namespace E_Commerce_Website.Core.IService
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> AddOrUpdateCategory(CategoryDto dto);
        Task<CategoryListResponseDto> GetAllCategories(string? name = null, bool? isActive = null);
        Task<CategoryListResponseDto?> GetCategoryById(int id);
        Task<CategoryResponseDto> DeleteCategory(int id);
    }
}
