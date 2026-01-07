using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IService;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddOrUpdateCategory")]
        public async Task<CategoryActionResponse> AddOrUpdateCategory([FromBody] CategoryRequest request)
        {
            return await _categoryService.AddOrUpdateCategory(request);
        }

        [HttpPost("GetAllCategories")]
        public async Task<CategoryPaginationResponse> GetAllCategories([FromBody]CategoryPaginationRequest request)
        {
            return await _categoryService.GetAllCategories(request);
        }

        [HttpPost("DeleteCategory")]
        public async Task<CategoryActionResponse> DeleteCategory([FromBody]DeleteCategoryRequest request)
        {
            return await _categoryService.DeleteCategory(request);
        }
    }
}
