using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;

namespace E_Commerce_Website.BI.MAP
{
    public class BrandMapper
    {
        // ADD
        public Brand BrandSaveMap(BrandRequest dto)
        {
            return new Brand
            {
                BrandName = dto.BrandName,
                IsPublished = dto.IsPublished ?? false,
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            };
        }

        // UPDATE
        public void BrandUpdateMap(Brand brand, BrandRequest dto)
        {
            brand.BrandName = dto.BrandName;
            brand.IsPublished = dto.IsPublished ?? brand.IsPublished;
            brand.IsActive = dto.IsActive;
            brand.ModifiedOn = DateTime.UtcNow;
        }

        // DELETE (SOFT DELETE)
        public void DeleteBrandMap(Brand brand)
        {
            brand.IsActive = false;
            brand.ModifiedOn = DateTime.UtcNow;
        }
    }
}
