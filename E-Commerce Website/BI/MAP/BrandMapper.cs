using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;
using E_Commerce_Website.Core.Entity;

namespace E_Commerce_Website.BI.MAP
{
    public class BrandMapper
    {
        public Brand AddBrandMapper(BrandDto dto)
        {
            return new Brand
            {
                BrandName = dto.BrandName,
                IsPublished = dto.IsPublished ?? false,
                IsActive = dto.IsActive,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = dto.BrandId
            };
        }

        public void UpdateBrandMapper(Brand brand, BrandDto dto)
        {
            brand.BrandName = dto.BrandName;
            brand.IsPublished = dto.IsPublished ?? brand.IsPublished;
            brand.IsActive = dto.IsActive;
            brand.ModifiedOn = DateTime.UtcNow;
            brand.ModifiedBy = dto.BrandId;
        }

        public static BrandDto MaptoDto(Brand brand)
        {
            return new BrandDto
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                IsPublished = brand.IsPublished,
                IsActive = brand.IsActive
            };
        }

        public static void DeleteMapper(Brand brand)
        {
            brand.IsActive = false;
            brand.ModifiedOn = DateTime.UtcNow;
        }
    }
}


