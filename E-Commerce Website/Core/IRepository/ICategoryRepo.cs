using E_Commerce_Website.Core.Enitities;

namespace E_Commerce_Website.Core.IRepository
{
    public interface ICategoryRepo
    {
        Task<int> AddCategory(Category category);
        Task<int> UpdateCategory(Category category);
        IQueryable<Category> GetAllCategories();
        Task<Category?> GetCategoryById(int categoryId);
    }
}
