using E_Commerce_Website.BI.MAP;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Core.IService;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.BI.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly CategoryMapper _mapper;
        public CategoryService(ICategoryRepo categoryRepo, CategoryMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDto> AddOUpdateCategory(CategoryDto dto)
        {
            var response = new CategoryResponseDto();
            try
            {
                if (dto.CategoryId == 0)
                {
                    var entity = _mapper.AddCategoryMapper(dto);
                    var categoryId = await _categoryRepo.AddCategory(entity);

                    response.CategoryId = categoryId;
                    response.Result = StatusResponse.Success.ToString();
                    response.Message = "User added successfully";
                }
                else
                {
                    var entity = await _categoryRepo.GetCategoryById(dto.CategoryId);

                    if (entity == null)
                    {
                        response.Result = StatusResponse.NotFound.ToString();
                        response.Message = "Category not found";
                        return response;
                    }

                    _mapper.UpdateCategoryMapper(dto, entity);
                    var categoryId = await _categoryRepo.UpdateCategory(entity);

                    response.CategoryId = categoryId;
                    response.Result = StatusResponse.Success.ToString();
                    response.Message = "Category updated successfully";
                }
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CategoryListResponseDto> GetAllCategories()
        {
            var response = new CategoryListResponseDto();
            try
            {
                var categories = _categoryRepo.GetAllCategories().Where(x => x.IsActive).ToList(); ;
                response.Categories = categories
                           .Select(x => _mapper.MapToDto(x))
                           .ToList();
                response.Result = StatusResponse.Success.ToString();
                response.Message = "Categories fetched successfully";
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<CategoryListResponseDto?> GetCategoryById(int id)
        {
            var response = new CategoryListResponseDto();
            try
            {
                var category = await _categoryRepo.GetCategoryById(id);

                if (category == null)
                {
                    response.Result = StatusResponse.NotFound.ToString();
                    response.Message = "Category not found";
                    return response;
                }

                response.Categories = new List<CategoryDto>
                {
                    _mapper.MapToDto(category)
                };

                response.Result = StatusResponse.Success.ToString();
                response.Message = "Category fetched successfully";
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<CategoryResponseDto> DeleteCategory(int id)
        {
            var response = new CategoryResponseDto();
            try
            {
                var category = await _categoryRepo.GetCategoryById(id);

                if (category == null)
                {
                    response.Result = StatusResponse.NotFound.ToString();
                    response.Message = "Category not found";
                    return response;
                }

                _mapper.DeleteCategoryMapper(category);
                await _categoryRepo.UpdateCategory(category); response.CategoryId = id;
                response.CategoryId = id;

                response.Result = StatusResponse.Success.ToString();
                response.Message = "Category deleted successfully";
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
                response.Message = ex.Message;
            }
            return response;
        }
    }
}



 