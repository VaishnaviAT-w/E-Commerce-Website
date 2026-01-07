using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce_Website.BI.MAP
{
    public class BrandMapper
    {
        public Brand BrandSaveMap(BrandRequest request, int UserId)
        {
            if (request == null)
                return null;

            return new Brand
            {
                BrandName = request.BrandName,
                IsPublished = request.IsPublished ?? false,
                IsActive = true,
                CreatedBy = UserId,
                CreatedOn = DateTime.UtcNow
            };
        }

        public Brand BrandUpdateMap(Brand entity, BrandRequest request, int UserId)
        {
            entity.BrandName = request.BrandName;
            entity.IsPublished = request.IsPublished ?? entity.IsPublished;
            entity.IsActive = request.IsActive;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = UserId;
            return entity;
        }

        public Brand BrandDeleteMap(Brand entity, int UserId)
        {
            entity.IsActive = false;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = UserId;
            return entity;
        }
    }
}
