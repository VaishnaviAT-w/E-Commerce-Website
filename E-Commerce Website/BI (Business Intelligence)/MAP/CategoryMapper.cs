using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;

namespace E_Commerce_Website.BI.MAP
{
    public class CategoryMapper
    {
        public Category CategorySaveMap(CategoryRequest request, int UserId)
        {
            if (request == null)
                return null;
            return new Category
            {
                CategoryName = request.CategoryName?.Trim(),
                IsPublished = request.IsPublished ?? false,
                IncludeInMenu = request.IncludeInMenu ?? false,
                DisplayOrder = request.DisplayOrder ?? 0,
                UserId = UserId,
                IsActive = true,
                CreatedBy = UserId,
                CreatedOn = DateTime.UtcNow
            };
        }

        public Category CategoryUpdateMap(Category entity, CategoryRequest request, int UserId)
        {
            entity.CategoryName = request.CategoryName?.Trim();
            entity.IsPublished = request.IsPublished ?? entity.IsPublished;
            entity.IncludeInMenu = request.IncludeInMenu ?? entity.IncludeInMenu;
            entity.DisplayOrder = request.DisplayOrder ?? entity.DisplayOrder;
            entity.IsActive = request.IsActive;
            entity.ModifiedBy = UserId;
            entity.UserId = UserId;
            entity.ModifiedOn = DateTime.UtcNow;
            return entity;
        }

        public Category CategoryDeleteMap(Category entity, int UserId)
        {
            entity.IsActive = false;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = UserId;
            return entity;
        }
    }
}

