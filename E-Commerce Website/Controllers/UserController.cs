using E_Commerce_Website.Core.Contract.IService;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Enum;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddOrUpdateUser")]
        public async Task<UserActionResponse> AddOrUpdateUser([FromBody] UsersRequest usersDto)
        {
            return await _userService.AddOrUpdateUsers(usersDto);
        }

        [HttpPost("GetAllUsers")]
        public async Task<UserPaginationResponse> GetAllUsers(PaginationRequest request)
        {
            return await _userService.GetAllUsers(request);
        }

        [HttpPost("DeleteUser")]
        public async Task<UserActionResponse> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}

