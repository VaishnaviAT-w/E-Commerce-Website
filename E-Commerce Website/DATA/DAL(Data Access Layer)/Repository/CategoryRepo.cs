using E_Commerce_Website.Core.Enitities;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Website.DATA.DAL_Data_Access_Layer_.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.CategoryId;
        }

        public async Task<int> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category.CategoryId;
        }

        public IQueryable<Category> GetAllCategories()
        {
            return _context.Categories.Where(x => x.IsActive);
        }

        public async Task<Category?> GetCategoryById(int categoryId)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.IsActive);
        }
    }
}
