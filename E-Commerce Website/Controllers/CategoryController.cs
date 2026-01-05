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
        public async Task<CategoryResponseDto> AddOrUpdateCategory([FromBody] CategoryDto categoryDto)
        {
            return await _categoryService.AddOrUpdateCategory(categoryDto);
        }

        [HttpGet("GetAllCategories")]   
        public async Task<CategoryListResponseDto> GetAllCategories()
        {
            return await _categoryService.GetAllCategories();
        }

        [HttpGet("GetCategoryById")]
        public async Task<CategoryListResponseDto?> GetCategoryById(int id)
        {
            return await _categoryService.GetCategoryById(id);
        }

        [HttpPost("DeleteCategory")]
        public async Task<CategoryResponseDto> DeleteCategory(int id)
        {
            return await _categoryService.DeleteCategory(id);
        }

    }
}
