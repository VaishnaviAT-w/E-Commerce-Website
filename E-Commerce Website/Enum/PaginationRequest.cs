namespace E_Commerce_Website.Enum
{
    public class PaginationRequest
    {
        public int Index { get; set; } 
        public int PageSize { get; set; } 
        public string? Search { get; set; }
    }

    public class PaginationResponse
    {
        public int Index { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
    }
}
