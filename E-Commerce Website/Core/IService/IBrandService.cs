using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Enum;

namespace E_Commerce_Website.Core.IService
{
    public interface IBrandService
    {
        Task<BrandActionResponse> AddOrUpdateBrand(BrandRequest dto);
        //Task<BrandPaginationResponse> GetAllBrands(PaginationRequest request, BrandFilterRequest filter);
        Task<BrandPaginationResponse> GetAllBrands(PaginationRequest request);

        Task<BrandActionResponse> DeleteBrand(int id);

        Task<List<BrandDropDown>> GetBrandDropDown();
    }
}
