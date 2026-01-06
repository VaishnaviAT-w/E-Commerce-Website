using E_Commerce_Website.BI.Mapper;
using E_Commerce_Website.Core.Contract.IRepository;
using E_Commerce_Website.Core.Contract.IService;
using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Data.Extensions;
using E_Commerce_Website.Enum;
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

        public async Task<UserActionResponse> AddOrUpdateUsers(UsersRequest request)
        {
            UserActionResponse response = new();

            // ADD
            if (request.Id == 0)
            {
                var entity = _mapper.UserSaveMap(request);

                response.UserId = await _userRepo.AddUsers(entity);
                response.Result = response.UserId > 0
                    ? StatusResponse.Success
                    : StatusResponse.Failed;

                response.Message = "User added successfully";
                return response;
            }

            // UPDATE
            var existingEntity = await _userRepo.GetUsersById(request.Id);
            if (existingEntity == null)
            {
                response.Result = StatusResponse.NotFound;
                response.Message = "User not found";
                return response;
            }

            _mapper.UserUpdateMap(existingEntity, request);

            response.UserId = await _userRepo.UpdateUsers(existingEntity);
            response.Result = response.UserId > 0
                ? StatusResponse.Success
                : StatusResponse.Failed;

            response.Message = "User updated successfully";
            return response;
        }


        //public async Task<UserPaginationResponse> GetAllUsers(PaginationRequest request)
        //{
        //    UserPaginationResponse response = new()
        //    {
        //        Index = request.Index,
        //        PageSize = request.PageSize
        //    };

        //    var usersQuery = _userRepo.GetAllUsers()
        //        .WhereIf(!string.IsNullOrEmpty(filter.Name),
        //            x => x.Fullname.ToLower().Contains(filter.Name.ToLower()))
        //        .WhereIf(!string.IsNullOrEmpty(filter.Email),
        //            x => x.Email.ToLower().Contains(filter.Email.ToLower()))
        //        .WhereIf(filter.IsActive.HasValue,
        //            x => x.IsActive == filter.IsActive.Value);

        //    response.TotalCount = usersQuery.Count();

        //    if (response.TotalCount == 0)
        //    {
        //        response.Result = StatusResponse.NotFound;
        //        return response;
        //    }

        //    response.Users = await usersQuery
        //    .AsNoTracking()
        //    .OrderByDescending(x => x.UserId)
        //    .Skip((request.Index - 1) * request.PageSize)
        //    .Take(request.PageSize)
        //    .Select(x => new UsersRequest
        //    {
        //        Id = x.UserId,
        //        FullName = x.Fullname,
        //        Email = x.Email,
        //        MobileNo = x.Mobile,
        //        Role = x.Role,
        //        IsActive = x.IsActive
        //    })
        //    .ToListAsync();


        //    response.PageCount =
        //        (response.TotalCount / request.PageSize) +
        //        (response.TotalCount % request.PageSize > 0 ? 1 : 0);

        //    response.Result = StatusResponse.Success;
        //    return response;
        //}


        public async Task<UserPaginationResponse> GetAllUsers(PaginationRequest request)
        {
            UserPaginationResponse response = new()
            {
                Index = request.Index,
                PageSize = request.PageSize
            };

            var usersQuery = _userRepo.GetAllUsers();

            response.TotalCount = usersQuery.Count();

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
                    Id = x.UserId,
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

        public async Task<UserActionResponse> DeleteUser(int id)
        {
            var response = new UserActionResponse();

            try
            {
                var user = await _userRepo.GetUsersById(id);

                if (user == null)
                {
                    response.Result = StatusResponse.NotFound;
                    return response;
                }

                _mapper.UserDeleteMap(user);
                await _userRepo.UpdateUsers(user);

                response.UserId = id;
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
