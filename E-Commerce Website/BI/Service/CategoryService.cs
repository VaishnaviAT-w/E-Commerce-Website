using E_Commerce_Website.BI.MAP;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Core.IService;
using E_Commerce_Website.Data.Extensions;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Add Or Update Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CategoryActionResponse> AddOrUpdateCategory(CategoryRequest request)
        {
            var response = new CategoryActionResponse();

            // ADD
            if (request.CategoryId == 0)
            {
                var entity = _mapper.CategorySaveMap(request, request.CategoryId);

                response.CategoryId = await _categoryRepo.AddCategory(entity);
                response.Result = response.CategoryId > 0
                    ? StatusResponse.Success
                    : StatusResponse.Failed;

                response.Message = "Category added successfully";
                return response;
            }

            // UPDATE
            var existingEntity = await _categoryRepo.GetCategoryById(request.CategoryId);
            if (existingEntity == null)
            {
                response.Result = StatusResponse.NotFound;
                response.Message = "Category not found";
                return response;
            }

            _mapper.CategoryUpdateMap(existingEntity, request,request.CategoryId);

            response.CategoryId = await _categoryRepo.UpdateCategory(existingEntity);
            response.Result = response.CategoryId > 0
                ? StatusResponse.Success
                : StatusResponse.Failed;

            response.Message = "Category updated successfully";
            return response;
        }

        /// <summary>
        /// Get All Categories with Pagination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CategoryPaginationResponse> GetAllCategories(CategoryPaginationRequest request)
        {
            CategoryPaginationResponse response = new()
            {
                Index = request.Index,
                PageSize = request.PageSize
            };

            var categoriesQuery = _categoryRepo.GetAllCategories()

           .WhereIf(!string.IsNullOrWhiteSpace(request.Search),x => x.CategoryName.ToLower().Contains(request.Search!.ToLower()))
           .WhereIf(request.IsActive.HasValue,x => x.IsActive == request.IsActive.Value).AsNoTracking();

            response.TotalCount = await categoriesQuery.CountAsync();

            if (response.TotalCount == 0)
            {
                response.Result = StatusResponse.NotFound;
                return response;
            }

            response.Categories = await categoriesQuery
                .OrderByDescending(x => x.CategoryId)
                .Skip((request.Index - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryRequest
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    IncludeInMenu=x.IncludeInMenu,
                    DisplayOrder=x.DisplayOrder,
                    IsPublished = x.IsPublished,
                    IsActive = x.IsActive
                })
                .AsNoTracking()
                .ToListAsync();

            response.PageCount =
                (response.TotalCount / request.PageSize) +
                (response.TotalCount % request.PageSize > 0 ? 1 : 0);

            response.Result = StatusResponse.Success;
            return response;
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CategoryActionResponse> DeleteCategory(DeleteCategoryRequest request)
        {
            var response = new CategoryActionResponse();
            try
            {
                var category = await _categoryRepo.GetCategoryById(request.CategoryId);

                if (category == null)
                {
                    response.Result = StatusResponse.NotFound;
                    return response;
                }

                _mapper.CategoryDeleteMap(category,request.CategoryId);
                await _categoryRepo.UpdateCategory(category); response.CategoryId = request.CategoryId;
                response.CategoryId = request.CategoryId;

                response.Result = StatusResponse.Success;
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}


 