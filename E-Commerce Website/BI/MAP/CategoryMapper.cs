using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;

namespace E_Commerce_Website.BI.MAP
{
    public class CategoryMapper
    {
        public Category CategorySaveMap(CategoryRequest request, int CategoryId)
        {
            if (request == null)
                return null;
            return new Category
            {
                CategoryName = request.CategoryName?.Trim(),
                IsPublished = request.IsPublished ?? false,
                IncludeInMenu = request.IncludeInMenu ?? false,
                DisplayOrder = request.DisplayOrder ?? 0,
                IsActive = true,
                CreatedBy = CategoryId,
                CreatedOn = DateTime.UtcNow
            };
        }

        public Category CategoryUpdateMap(Category entity, CategoryRequest request, int CategoryId)
        {
            entity.CategoryName = request.CategoryName?.Trim();
            entity.IsPublished = request.IsPublished ?? entity.IsPublished;
            entity.IncludeInMenu = request.IncludeInMenu ?? entity.IncludeInMenu;
            entity.DisplayOrder = request.DisplayOrder ?? entity.DisplayOrder;
            entity.IsActive = request.IsActive;
            entity.ModifiedBy = CategoryId;
            entity.ModifiedOn = DateTime.UtcNow;
            return entity;
        }

        public Category CategoryDeleteMap(Category entity, int CategoryId)
        {
            entity.IsActive = false;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = CategoryId;
            return entity;
        }
    }
}

