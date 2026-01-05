using E_Commerce_Website.Core.DTO;

namespace E_Commerce_Website.Core.Contract.IService
{
    public interface IUserService
    {
        Task<AddUserResponseDto> AddOrUpdateUsers(UsersDto usersDto);
        Task<UserListResponseDto> GetByIdUser(int id);
        Task<UserListResponseDto> GetAllUsers(string? name = null, string? email = null, bool? isActive = null);
        Task<DeleteUserResponseDto> DeleteUser(int id);
    }
}
