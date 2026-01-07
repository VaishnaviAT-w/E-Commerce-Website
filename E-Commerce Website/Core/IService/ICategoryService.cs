using E_Commerce_Website.Core.DTO;

namespace E_Commerce_Website.Core.IService
{
    public interface ICategoryService
    {
        Task<CategoryActionResponse> AddOrUpdateCategory(CategoryRequest request);
        Task<CategoryPaginationResponse> GetAllCategories(CategoryPaginationRequest request);
        Task<CategoryActionResponse> DeleteCategory(DeleteCategoryRequest request);
    }
}
