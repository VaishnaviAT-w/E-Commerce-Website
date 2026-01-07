using E_Commerce_Website.Enum;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.Core.DTO
{
    public class BrandRequest
    {
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public bool? IsPublished { get; set; }
        public bool IsActive { get; set; }
    }

    public class BrandActionResponse
    {
        public int BrandId { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }

    public class BrandPaginationRequest : PaginationRequest
    {
        public bool? IsActive { get; set; }
        public bool? IsPublished { get; set; }
    }

    public class DeleteBrandRequest
    {
        public int BrandId { get; set; }
    }

    public class BrandPaginationResponse : PaginationResponse
    {
        public List<BrandRequest>? Brands { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }

    public class BrandDropDown()
    {
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public bool? IsPublished { get; set; }
    }
}

