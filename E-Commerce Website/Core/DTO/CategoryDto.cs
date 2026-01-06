using E_Commerce_Website.Enum;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.Core.DTO
{
    public class CategoryRequest
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IncludeInMenu { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class CategoryActionResponse
    {
        public int CategoryId { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }

    public class CategoryFilterRequest
    {
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
    }


    public class CategoryPaginationResponse : PaginationResponse
    {
        public List<CategoryRequest>? Categories { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }
}
