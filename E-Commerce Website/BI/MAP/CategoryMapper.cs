using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Enitities;
using E_Commerce_Website.Core.Entity;

namespace E_Commerce_Website.BI.MAP
{
    public class CategoryMapper
    {
        public Category AddCategoryMapper(CategoryDto dto)
        {
            return new Category
            {
                CategoryId = dto.CategoryId,
                CategoryName = dto.CategoryName,
                IsPublished = dto.IsPublished ?? false,
                IncludeInMenu = dto.IncludeInMenu ?? false,
                DisplayOrder = dto.DisplayOrder ?? 0,
                IsActive = dto.IsActive,
                CreatedOn = DateTime.UtcNow,
            };
        }

        public Category UpdateCategoryMapper(CategoryDto dto, Category category)
        {
            category.CategoryId = dto.CategoryId;
            category.CategoryName = dto.CategoryName;
            category.IsPublished = dto.IsPublished ?? category.IsPublished;
            category.IncludeInMenu = dto.IncludeInMenu ?? category.IncludeInMenu;
            category.DisplayOrder = dto.DisplayOrder ?? category.DisplayOrder;
            category.IsActive = dto.IsActive;
            category.ModifiedOn = DateTime.UtcNow;
            return category;
        }

        public CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IsPublished = category.IsPublished,
                IncludeInMenu = category.IncludeInMenu,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive
            };
        }

        public void DeleteCategoryMapper(Category category)
        {
            category.IsActive = false;
            category.ModifiedOn = DateTime.UtcNow;
        }
    }
}

