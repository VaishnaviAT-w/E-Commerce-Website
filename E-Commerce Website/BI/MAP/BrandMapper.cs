using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce_Website.BI.MAP
{
    public class BrandMapper
    {
        public Brand BrandSaveMap(BrandRequest request, int BrandId)
        {
            if (request == null)
                return null;
            return new Brand
            {
                BrandName = request.BrandName,
                IsPublished = request.IsPublished ?? false,
                IsActive = true,
                CreatedBy = BrandId,
                CreatedOn = DateTime.UtcNow
            };
        }

        public Brand BrandUpdateMap(Brand entity, BrandRequest request, int BrandId)
        {
            entity.BrandName = request.BrandName;
            entity.IsPublished = request.IsPublished ?? entity.IsPublished;
            entity.IsActive = request.IsActive;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.CreatedBy = BrandId;
            return entity;
        }

        public Brand BrandDeleteMap(Brand entity, int BrandId)
        {
            entity.IsActive = false;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.CreatedBy = BrandId;
            return entity;
        }
    }
}
