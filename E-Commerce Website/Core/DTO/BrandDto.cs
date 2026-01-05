namespace E_Commerce_Website.Core.DTO
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public bool? IsPublished { get; set; }
        public bool IsActive { get; set; }

    }

    public class BrandListResponseDto
    {
        public List<BrandDto>? Brands { get; set; }
        public string? Result { get; set; }
        public string? Message { get; set; }
    }

    public class BrandResponseDto
    {
        public int BrandId { get; set; }
        public string? Result { get; set; }
        public string? Message { get; set; }
    }

    public class DeleteBrandResponseDto
    {
        public int BrandId { get; set; }
        public string? Result { get; set; }
        public string? Message { get; set; }
    }
}

