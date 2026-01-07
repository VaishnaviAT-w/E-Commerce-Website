using E_Commerce_Website.Core.DTO;

namespace E_Commerce_Website.Core.Contract.IService
{
    public interface IUserService
    {
        Task<UserActionResponse> AddOrUpdateUsers(UsersRequest request);
        Task<UserPaginationResponse> GetAllUsers(UserPaginationRequest request);
        Task<UserActionResponse> DeleteUser(DeleteUserRequest request);
    }
}
