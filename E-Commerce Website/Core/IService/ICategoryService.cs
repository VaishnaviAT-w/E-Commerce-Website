using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Enum;

namespace E_Commerce_Website.Core.IService
{
    public interface ICategoryService
    {
        Task<CategoryActionResponse> AddOrUpdateCategory(CategoryRequest request);
        //Task<CategoryPaginationResponse> GetAllCategories(PaginationRequest request, UserFilterRequest filter);
        Task<CategoryPaginationResponse> GetAllCategories(PaginationRequest request);
        Task<CategoryActionResponse> DeleteCategory(int id);
    }
}
