using E_Commerce_Website.BI.Mapper;
using E_Commerce_Website.Core.Contract.IRepository;
using E_Commerce_Website.Core.Contract.IService;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Data.Extensions;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Add Or Update User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserActionResponse> AddOrUpdateUsers(UsersRequest request)
        {
            UserActionResponse response = new();
            try
            {
                // ADD
                if (request.UserId == 0)
                {
                    var entity = _mapper.UserSaveMap(request, request.UserId);

                    response.UserId = await _userRepo.AddUsers(entity);
                    response.Result = response.UserId > 0
                        ? StatusResponse.Success
                        : StatusResponse.Failed;

                    return response;
                }
                // UPDATE
                var existingEntity = await _userRepo.GetUsersById(request.UserId);
                if (existingEntity == null)
                {
                    response.Result = StatusResponse.NotFound;
                    return response;
                }

                _mapper.UserUpdateMap(existingEntity, request, request.UserId);

                response.UserId = await _userRepo.UpdateUsers(existingEntity);
                response.Result = response.UserId > 0
                    ? StatusResponse.Success
                    : StatusResponse.Failed;

                return response;
            }
            catch(Exception ex)
            {
                response.Result = StatusResponse.Failed;
                return response;
            }
        }

        /// <summary>
        /// Get All Users with Pagination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserPaginationResponse> GetAllUsers(UserPaginationRequest request)
        {
            UserPaginationResponse response = new()
            {
                Index = request.Index,
                PageSize = request.PageSize
            };

            var usersQuery = _userRepo.GetAllUsers()

           .WhereIf(!string.IsNullOrWhiteSpace(request.Search),x => x.Fullname.ToLower().Contains(request.Search!.ToLower()) ||
                    x.Email.ToLower().Contains(request.Search!.ToLower()))
           .WhereIf(request.IsActive.HasValue, x => x.IsActive == request.IsActive.Value).AsNoTracking();

            response.TotalCount = await usersQuery.CountAsync();

            if (response.TotalCount == 0)
            {
                response.Result = StatusResponse.NotFound;
                return response;
            }

            response.Users = await usersQuery
            .OrderByDescending(x => x.UserId)
            .Skip((request.Index - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new UsersRequest
            {
                UserId = x.UserId,
                FullName = x.Fullname,
                Email = x.Email,
                MobileNo = x.Mobile,
                Role = x.Role,
                IsActive = x.IsActive
            })
            .AsNoTracking()
            .ToListAsync();

            response.PageCount =
                (response.TotalCount / request.PageSize) +
                (response.TotalCount % request.PageSize > 0 ? 1 : 0);

            response.Result = StatusResponse.Success;
            return response;
        }

        /// <summary>
        /// delete User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserActionResponse> DeleteUser(DeleteUserRequest request)
        {
            var response = new UserActionResponse();

            try
            {
                var user = await _userRepo.GetUsersById(request.UserId);

                if (user == null)
                {
                    response.Result = StatusResponse.NotFound;
                    return response;
                }

                _mapper.UserDeleteMap(user,request.UserId);
                await _userRepo.UpdateUsers(user);
                response.UserId = request.UserId;

                response.Result = StatusResponse.Success;
            }
            catch (Exception ex)
            {
                response.Result = StatusResponse.Failed;
            }
            return response;
        }
    }
}
