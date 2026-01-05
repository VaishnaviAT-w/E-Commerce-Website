using E_Commerce_Website.BI.MAP;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Data.Extensions;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Core.IService;
using System.Linq.Expressions;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.BI.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepo _brandRepo;
        private readonly BrandMapper _brandmapper;
        public BrandService(IBrandRepo brandRepo,BrandMapper brandMapper)
        {
            _brandRepo = brandRepo;
            _brandmapper = brandMapper;
        }

        public async Task<BrandResponseDto> AddOrUpdateBrand(BrandDto dto)
        {
            var response = new BrandResponseDto();

            try
            {
                // ADD
                if (dto.BrandId == 0)
                {
                    var entity = _brandmapper.AddBrandMapper(dto);
                    var brandId = await _brandRepo.AddBrand(entity);

                    response.BrandId = brandId;
                    response.Result = StatusResponse.Success;
                    response.Message = "Brand added successfully";
                }
                // UPDATE
                else
                {
                    var entity = await _brandRepo.GetBrandById(dto.BrandId);

                    if (entity == null)
                    {
                        response.Result = StatusResponse.NotFound;
                        response.Message = "Brand not found";
                        return response;
                    }

                    _brandmapper.UpdateBrandMapper(entity, dto);
                    var brandId = await _brandRepo.UpdateBrand(entity);

                    response.BrandId = brandId;
                    response.Result = StatusResponse.Success;
                    response.Message = "Brand updated successfully";
                }
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed;
                response.Message = ex.Message;
            }

            return response;
        }

        public IQueryable<BrandListResponseDto> GetAllBrands(bool? isPublished = null)
        {
            return _brandRepo.GetAllBrands()
                .WhereIf(isPublished.HasValue, b => b.IsPublished == isPublished.Value)
                .Select(b => new BrandListResponseDto
                {
                    Brands = new List<BrandDto>
                    {
                        BrandMapper.MaptoDto(b) 
                    },
                    Result = StatusResponse.Success,
                    Message = "Brands retrieved successfully"
                })
                .AsQueryable();
        }

        //public IQueryable<BrandListResponseDto> GetAllBrands()
        //{
        //    var brands = _brandRepo.GetAllBrands();
        //    var response = brands.Select(b => new BrandListResponseDto
        //    {
        //        Brands = new List<BrandDto>
        //        {
        //            new BrandDto
        //            {
        //                BrandId = b.BrandId,
        //                BrandName = b.BrandName,
        //                IsPublished = b.IsPublished,
        //                IsActive = b.IsActive
        //            }
        //        },
        //        Result = StatusResponse.Success,
        //        Message = "Brands retrieved successfully"
        //    });
        //    return response;
        //}

        public async Task<BrandListResponseDto?> GetBrandById(int id)
        {
            var brand = await _brandRepo.GetBrandById(id);
            if (brand == null)
            {
                return new BrandListResponseDto
                {
                    Brands = null,
                    Result = StatusResponse.NotFound,
                    Message = "Brand not found"
                };
            }
            var response = new BrandListResponseDto
            {
                Brands = new List<BrandDto>
                {
                    new BrandDto
                    {
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName,
                        IsPublished = brand.IsPublished,
                        IsActive = brand.IsActive
                    }
                },
                Result = StatusResponse.Success,
                Message = "Brand retrieved successfully"
            };
            return response;
        }

        public  async Task<DeleteBrandResponseDto> DeleteBrand(int id)
        {
            var response = new DeleteBrandResponseDto();
            try
            {
                var brand = await _brandRepo.GetBrandById(id);
                if (brand == null)
                {
                    response.Result = StatusResponse.NotFound;
                    response.Message = "Brand not found";
                    return response;
                }
                brand.IsActive = false;
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