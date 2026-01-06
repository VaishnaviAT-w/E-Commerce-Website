using E_Commerce_Website.BI.MAP;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Core.IService;
using E_Commerce_Website.Data.Extensions;
using E_Commerce_Website.Enum;
using Microsoft.EntityFrameworkCore;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.BI.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepo _brandRepo;
        private readonly BrandMapper _mapper;
        public BrandService(IBrandRepo brandRepo, BrandMapper brandMapper)
        {
            _brandRepo = brandRepo;
            _mapper = brandMapper;
        }

        public async Task<BrandActionResponse> AddOrUpdateBrand(BrandRequest request)
        {
            BrandActionResponse response = new();

            // ADD
            if (request.BrandId == 0)
            {
                var entity = _mapper.BrandSaveMap(request);

                response.BrandId = await _brandRepo.AddBrand(entity);
                response.Result = response.BrandId > 0
                    ? StatusResponse.Success
                    : StatusResponse.Failed;

                response.Message = "Brand added successfully";
                return response;
            }

            // UPDATE
            var existingEntity = await _brandRepo.GetBrandById(request.BrandId);
            if (existingEntity == null)
            {
                response.Result = StatusResponse.NotFound;
                response.Message = "Brand not found";
                return response;
            }

            _mapper.BrandUpdateMap(existingEntity, request);

            response.BrandId = await _brandRepo.UpdateBrand(existingEntity);
            response.Result = response.BrandId > 0
                ? StatusResponse.Success
                : StatusResponse.Failed;

            response.Message = "Brand updated successfully";
            return response;
        }

        //public async Task<BrandPaginationResponse> GetAllBrands(PaginationRequest request, BrandFilterRequest filter)
        //{
        //    BrandPaginationResponse response = new()
        //    {
        //        Index = request.Index,
        //        PageSize = request.PageSize
        //    };

        //    var brandsQuery = _brandRepo.GetAllBrands()
        //        .WhereIf(!string.IsNullOrEmpty(filter.Name),
        //    x => x.BrandName.ToLower().Contains(filter.Name.ToLower()))
        //        .WhereIf(filter.IsPublished.HasValue,
        //    x => x.IsPublished == filter.IsPublished.Value)
        //        .WhereIf(filter.IsActive.HasValue,
        //    x => x.IsActive == filter.IsActive.Value);


        //    response.TotalCount = brandsQuery.Count();

        //    if (response.TotalCount == 0)
        //    {
        //        response.Result = StatusResponse.NotFound;
        //        return response;
        //    }

        //    response.Brands = await brandsQuery
        //        .AsNoTracking()
        //        .OrderByDescending(x => x.BrandId)
        //        .Skip((request.Index - 1) * request.PageSize)
        //        .Take(request.PageSize)
        //        .Select(x => new BrandRequest
        //        {
        //            BrandId = x.BrandId,
        //            BrandName = x.BrandName,
        //            IsPublished = x.IsPublished,
        //            IsActive = x.IsActive
        //        })
        //        .ToListAsync();

        //    response.PageCount =
        //        (response.TotalCount / request.PageSize) +
        //        (response.TotalCount % request.PageSize > 0 ? 1 : 0);

        //    response.Result = StatusResponse.Success;
        //    return response;
        //}

        public async Task<BrandPaginationResponse> GetAllBrands(PaginationRequest request)
        {
            BrandPaginationResponse response = new()
            {
                Index = request.Index,
                PageSize = request.PageSize
            };

            var query = _brandRepo.GetAllBrands();

            response.TotalCount = query.Count();

            if (response.TotalCount == 0)
            {
                response.Result = StatusResponse.NotFound;
                response.Message = "No brands found";
                return response;
            }

            response.Brands = await query
                .OrderByDescending(x => x.BrandId)
                .Skip((request.Index - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BrandRequest
                {
                    BrandId = x.BrandId,
                    BrandName = x.BrandName,
                    IsPublished = x.IsPublished,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            response.PageCount =
                (response.TotalCount / request.PageSize) +
                (response.TotalCount % request.PageSize > 0 ? 1 : 0);

            response.Result = StatusResponse.Success;
            response.Message = "Brands fetched successfully";

            return response;
        }

        public async Task<List<BrandDropDown>> GetBrandDropDown()
        {
            return await _brandRepo.GetAllBrands()
        .Where(x => x.IsActive == true)
        .OrderBy(x => x.BrandName)
        .Select(x => new BrandDropDown
        {
            BrandId = x.BrandId,
            BrandName = x.BrandName,
            IsPublished = x.IsPublished,
        })
        .ToListAsync();
        }


        public async Task<BrandActionResponse> DeleteBrand(int id)
        {
            var response = new BrandActionResponse();
            try
            {
                var brand = await _brandRepo.GetBrandById(id);
                if (brand == null)
                {
                    response.Result = StatusResponse.NotFound;
                    response.Message = "Brand not found";
                    return response;
                }

                _mapper.DeleteBrandMap(brand);
                await _brandRepo.UpdateBrand(brand);
                response.BrandId = id;

                response.Result = StatusResponse.Success;
                response.Message = "Brand deleted successfully";
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