using E_Commerce_Website.Core.Contract.IService;
using E_Commerce_Website.Core.DTO;
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
        public async Task<AddUserResponseDto> AddOrUpdateUser([FromBody] UsersDto usersDto)
        {
            return await _userService.AddOrUpdateUsers(usersDto);
        }

        [HttpGet("GetAllUsers")]
        public async Task<UserListResponseDto> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("GetUserById")]
        public async Task<UserListResponseDto> GetUserById(int id)
        {
            return await _userService.GetByIdUser(id);
        }

        [HttpPost("DeleteUser")]
        public async Task<DeleteUserResponseDto> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}

