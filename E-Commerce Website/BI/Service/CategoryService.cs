using E_Commerce_Website.BI.MAP;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Core.IService;
using E_Commerce_Website.Enum;
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

        public async Task<CategoryActionResponse> AddOrUpdateCategory(CategoryRequest request)
        {
            var response = new CategoryActionResponse();

            // ADD
            if (request.CategoryId == 0)
            {
                var entity = _mapper.CategorySaveMap(request);

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

            _mapper.CategoryUpdateMap(existingEntity, request);

            response.CategoryId = await _categoryRepo.UpdateCategory(existingEntity);
            response.Result = response.CategoryId > 0
                ? StatusResponse.Success
                : StatusResponse.Failed;

            response.Message = "Category updated successfully";
            return response;
        }

        //public async Task<CategoryPaginationResponse> GetAllCategories(PaginationRequest request, UserFilterRequest filter)
        //{
        //    CategoryPaginationResponse response = new()
        //    {
        //        Index = request.Index,
        //        PageSize = request.PageSize
        //    };

        //    var categoriesQuery = _categoryRepo.GetAllCategories()
        //        .WhereIf(!string.IsNullOrEmpty(filter.Name),
        //            x => x.CategoryName.ToLower().Contains(filter.Name.ToLower()))
        //        .WhereIf(filter.IsActive.HasValue,
        //            x => x.IsActive == filter.IsActive.Value);

        //    response.TotalCount = categoriesQuery.Count();

        //    if (response.TotalCount == 0)
        //    {
        //        response.Result = StatusResponse.NotFound;
        //        return response;
        //    }

        //    response.Categories = await categoriesQuery
        //        .OrderByDescending(x => x.CategoryId)
        //        .Skip((request.Index - 1) * request.PageSize)
        //        .Take(request.PageSize)
        //        .Select(x => new CategoryRequest
        //        {
        //            CategoryId = x.CategoryId,
        //            CategoryName = x.CategoryName,
        //            IsPublished = x.IsPublished,
        //            IsActive = x.IsActive
        //        })
        //        .AsNoTracking()
        //        .ToListAsync();

        //    response.PageCount =
        //        (response.TotalCount / request.PageSize) +
        //        (response.TotalCount % request.PageSize > 0 ? 1 : 0);

        //    response.Result = StatusResponse.Success;
        //    return response;
        //}

        public async Task<CategoryPaginationResponse> GetAllCategories(PaginationRequest request)
        {
            var response = new CategoryPaginationResponse()
            {
                Index = request.Index,
                PageSize = request.PageSize
            };

            var query = _categoryRepo.GetAllCategories();

            response.TotalCount = query.Count();

            if (response.TotalCount == 0)
            {
                response.Result = StatusResponse.NotFound;
                response.Message = "No categories found";
                return response;
            }

            response.Categories = await query
                .OrderByDescending(x => x.CategoryId)
                .Skip((request.Index - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryRequest
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    IsPublished = x.IsPublished,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            response.PageCount =
                (response.TotalCount / request.PageSize) +
                (response.TotalCount % request.PageSize > 0 ? 1 : 0);

            response.Result = StatusResponse.Success;
            response.Message = "Categories fetched successfully";

            return response;
        }


        public async Task<CategoryActionResponse> DeleteCategory(int id)
        {
            var response = new CategoryActionResponse();
            try
            {
                var category = await _categoryRepo.GetCategoryById(id);

                if (category == null)
                {
                    response.Result = StatusResponse.NotFound;
                    response.Message = "Category not found";
                    return response;
                }

                _mapper.CategoryDeleteMap(category);
                await _categoryRepo.UpdateCategory(category); response.CategoryId = id;
                response.CategoryId = id;

                response.Result = StatusResponse.Success;
                response.Message = "Category deleted successfully";
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


 