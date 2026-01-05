using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;

namespace E_Commerce_Website.Core.IService
{
    public interface IBrandService
    {
        Task<BrandResponseDto> AddOrUpdateBrand(BrandDto dto);
        IQueryable<BrandListResponseDto> GetAllBrands(bool? isPublished = null);
        Task<BrandListResponseDto?> GetBrandById(int id);
        Task<DeleteBrandResponseDto> DeleteBrand(int id);
    }
}
