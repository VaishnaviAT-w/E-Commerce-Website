using E_Commerce_Website.BI.Mapper;
using E_Commerce_Website.Core.Contract.IRepository;
using E_Commerce_Website.Core.Contract.IService;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Entity;
using static E_Commerce_Website.Data.Enum.EnumResponse;

namespace E_Commerce_Website.BI.Service
{
    public class UserService : IUserService
    {
        private readonly IUsersRepo _userRepo;
        private readonly UserMapper _mapper;

        public UserService(IUsersRepo userRepo, UserMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<AddUserResponseDto> AddOrUpdateUsers(UsersDto usersDto)
        {
            var response = new AddUserResponseDto();
            try
            {
                // ADD
                if (usersDto.Id == 0)
                {
                    var entity = _mapper.AddUserMapper(usersDto);
                    var userId = await _userRepo.AddUsers(entity);

                    response.UserId = userId;
                    response.Result = StatusResponse.Success.ToString();
                    response.Message = "User added successfully";
                }
                // UPDATE
                else
                {
                    var entity = await _userRepo.GetUsersById(usersDto.Id);

                    if (entity == null)
                    {
                        response.Result = StatusResponse.NotFound.ToString();
                        response.Message = "User not found";
                        return response;
                    }

                    _mapper.UpdateUserMapper(entity, usersDto);
                    var userId = await _userRepo.UpdateUsers(entity);

                    response.UserId = userId;
                    response.Result = StatusResponse.Success.ToString();
                    response.Message = "User updated successfully";
                }
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<UserListResponseDto> GetAllUsers()
        {
            var response = new UserListResponseDto();

            try
            {
                var users = _userRepo
                    .GetAllUsers()
                    .Where(x => x.IsActive == true)
                    .ToList();

                response.Users = users
                    .Select(UserMapper.MaptoDto)
                    .ToList();

                response.Result = StatusResponse.Success.ToString();
                response.Message = "Users get successfully";
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<UserListResponseDto> GetByIdUser(int id)
        {
            var response = new UserListResponseDto();

            try
            {
                var user = await _userRepo.GetUsersById(id);

                if (user == null)
                {
                    response.Result = StatusResponse.NotFound.ToString();
                    response.Message = "User not found";
                    return response;
                }

                response.Users = new List<UsersDto>
                {
                      UserMapper.MaptoDto(user)
                };

                response.Result = StatusResponse.Success.ToString();
                response.Message = "User retrieved successfully";
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<DeleteUserResponseDto> DeleteUser(int id)
        {
            var response = new DeleteUserResponseDto();

            try
            {
                var user = await _userRepo.GetUsersById(id);

                if (user == null)
                {
                    response.Result = StatusResponse.NotFound.ToString();
                    return response;
                }

                UserMapper.DeleteMapper(user);
                await _userRepo.UpdateUsers(user);

                response.UserId = id;
                response.Result = StatusResponse.Success.ToString();
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed.ToString();
            }

            return response;
        }
    }
}
