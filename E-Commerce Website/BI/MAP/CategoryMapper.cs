using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;

namespace E_Commerce_Website.BI.MAP
{
    public class CategoryMapper
    {
        // ADD
        public Category CategorySaveMap(CategoryRequest request)
        {
            return new Category
            {
                CategoryName = request.CategoryName?.Trim(),
                IsPublished = request.IsPublished ?? false,
                IncludeInMenu = request.IncludeInMenu ?? false,
                DisplayOrder = request.DisplayOrder ?? 0,
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            };
        }

        // UPDATE
        public Category CategoryUpdateMap(Category entity, CategoryRequest request)
        {
            entity.CategoryName = request.CategoryName?.Trim();
            entity.IsPublished = request.IsPublished ?? entity.IsPublished;
            entity.IncludeInMenu = request.IncludeInMenu ?? entity.IncludeInMenu;
            entity.DisplayOrder = request.DisplayOrder ?? entity.DisplayOrder;
            entity.IsActive = request.IsActive;
            entity.ModifiedOn = DateTime.UtcNow;
            return entity;
        }

        // DELETE
        public Category CategoryDeleteMap(Category entity)
        {
            entity.IsActive = false;
            entity.ModifiedOn = DateTime.UtcNow;
            return entity;
        }
    }
}

