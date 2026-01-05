using E_Commerce_Website.Core.DTO;

namespace E_Commerce_Website.Core.IService
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> AddOUpdateCategory(CategoryDto dto);
        Task<CategoryListResponseDto> GetAllCategories();
        Task<CategoryListResponseDto?> GetCategoryById(int id);
        Task<CategoryResponseDto> DeleteCategory(int id);
    }
}
