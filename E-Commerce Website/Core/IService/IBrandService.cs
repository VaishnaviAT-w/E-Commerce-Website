using E_Commerce_Website.Core.DTO;

namespace E_Commerce_Website.Core.IService
{
    public interface IBrandService
    {
        Task<BrandActionResponse> AddOrUpdateBrand(BrandRequest dto);
        Task<BrandPaginationResponse> GetAllBrands(BrandPaginationRequest request);
        Task<BrandActionResponse> DeleteBrand(DeleteBrandRequest request);
        Task<List<BrandDropDown>> GetBrandDropDown();
    }
}
