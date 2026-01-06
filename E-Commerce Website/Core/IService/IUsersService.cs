using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Enum;

namespace E_Commerce_Website.Core.Contract.IService
{
    public interface IUserService
    {
        Task<UserActionResponse> AddOrUpdateUsers(UsersRequest request);
        //Task<UserPaginationResponse> GetAllUsers(PaginationRequest request, UserFilterRequest filter);

       Task<UserPaginationResponse> GetAllUsers(PaginationRequest request);

       Task<UserActionResponse> DeleteUser(int id);
    }
}
