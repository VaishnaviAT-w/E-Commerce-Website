using E_Commerce_Website.BI.MAP;
using E_Commerce_Website.Core.Contract.IService;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Core.IService;
using E_Commerce_Website.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.BI.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepo _brandRepo;
        private readonly BrandMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public BrandService(IBrandRepo brandRepo, BrandMapper brandMapper, ICurrentUserService currentUser)
        {
            _brandRepo = brandRepo;
            _mapper = brandMapper;
            _currentUser = currentUser;
        }

        /// <summary>
        /// Add Or Update Brand 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BrandActionResponse> AddOrUpdateBrand(BrandRequest request)
        {
            var response = new BrandActionResponse();
            try
            {
                int userId = _currentUser.UserId;
                // ADD
                if (request.BrandId == 0)
                {
                    var entity = _mapper.BrandSaveMap(request, _currentUser.UserId);

                    response.BrandId = await _brandRepo.AddBrand(entity);
                    response.Result = response.BrandId > 0
                        ? StatusResponse.Success
                        : StatusResponse.Failed;

                    return response;
                }
                // UPDATE
                var existingEntity = await _brandRepo.GetBrandById(request.BrandId);
                if (existingEntity == null)
                {
                    response.Result = StatusResponse.NotFound;
                    return response;
                }

                _mapper.BrandUpdateMap(existingEntity, request, _currentUser.UserId);

                response.BrandId = await _brandRepo.UpdateBrand(existingEntity);
                response.Result = response.BrandId > 0
                    ? StatusResponse.Success
                    : StatusResponse.Failed;

                return response;
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed;
                return response;
            }
        }

        /// <summary>
        /// Get All Brands with Pagination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BrandPaginationResponse> GetAllBrands(BrandPaginationRequest request)
        {
            BrandPaginationResponse response = new()
            {
                Index = request.Index,
                PageSize = request.PageSize
            };

            var brandsQuery = _brandRepo.GetAllBrands()

            .WhereIf(!string.IsNullOrWhiteSpace(request.Search), x => x.BrandName.ToLower().Contains(request.Search!.ToLower()))
            .WhereIf(request.IsPublished.HasValue, x => x.IsPublished == request.IsPublished.Value)
            .WhereIf(request.IsActive.HasValue, x => x.IsActive == request.IsActive.Value);

            response.TotalCount = await brandsQuery.CountAsync();

            if (response.TotalCount == 0)
            {
                response.Result = StatusResponse.NotFound;
                return response;
            }

            response.Brands = await brandsQuery
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
                .AsNoTracking()
                .ToListAsync();  

            response.PageCount =
                (response.TotalCount / request.PageSize) +
                (response.TotalCount % request.PageSize > 0 ? 1 : 0);

            response.Result = StatusResponse.Success;
            return response;
        }

        /// <summary>
        /// Get Brand DropDown
        /// </summary>
        /// <returns></returns>
        public async Task<List<BrandDropDown>> GetBrandDropDown()
        {
            var brands = await _brandRepo.GetActiveBrandsAsync();

            return brands.Select(x => new BrandDropDown
            {
                BrandId = x.BrandId,
                BrandName = x.BrandName,
                IsPublished = x.IsPublished
            })
             .ToList();
        }

        /// <summary>
        /// Delete Brand
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BrandActionResponse> DeleteBrand(DeleteBrandRequest request)
        {
            var response = new BrandActionResponse();
            try
            {
                var brand = await _brandRepo.GetBrandById(request.BrandId);
                if (brand == null)
                {   
                    response.Result = StatusResponse.NotFound;
                    return response;
                }

                _mapper.BrandDeleteMap(brand, _currentUser.UserId);
                await _brandRepo.UpdateBrand(brand);
                response.BrandId = request.BrandId;

                response.Result = StatusResponse.Success;
            }

            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed;
            }
            return response;
        }
    }
}