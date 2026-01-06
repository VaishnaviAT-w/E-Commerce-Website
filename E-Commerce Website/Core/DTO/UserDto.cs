using E_Commerce_Website.Enum;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.Core.DTO
{
    public class UsersRequest
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? MobileNo { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserActionResponse
    {
        public int UserId { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }

    public class UserFilterRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UserPaginationResponse : PaginationResponse
    {
        public List<UsersRequest>? Users { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }
}
