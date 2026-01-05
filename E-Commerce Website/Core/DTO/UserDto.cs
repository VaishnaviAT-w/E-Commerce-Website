using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.Core.DTO
{
    public class UsersDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Emails { get; set; }
        public string? Password { get; set; }
        public string? MobileNo { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class AddUserResponseDto
    {
        public int UserId { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }

    public class UserListResponseDto
    {
        public List<UsersDto>? Users { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }

    public class DeleteUserResponseDto
    {
        public int UserId { get; set; }
        public StatusResponse Result { get; set; }
        public string? Message { get; set; }
    }
}
