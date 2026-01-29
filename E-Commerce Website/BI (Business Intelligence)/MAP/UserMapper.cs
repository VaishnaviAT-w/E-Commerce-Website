using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Entity;

namespace E_Commerce_Website.BI.Mapper
{
    public class UserMapper
    {
        public Users UserSaveMap(UsersRequest request, int UserId)
        {
            if (request == null)
                return null;
            return new Users
            {
                Fullname = request.FullName?.Trim(),
                Email = request.Email,
                PasswordHash = request.Password,
                Mobile = request.MobileNo,
                Role = request.Role,
                IsActive = true,
                CreatedBy = UserId,
                CreatedOn = DateTime.UtcNow
            };
        }

        public Users UserUpdateMap(Users entity, UsersRequest request, int USerId)
        {
            entity.Fullname = request.FullName?.Trim();
            entity.Email = request.Email;
            entity.Mobile = request.MobileNo;
            entity.Role = request.Role;
            entity.IsActive = request.IsActive;
            entity.ModifyOn = DateTime.UtcNow;
            entity.ModifyBy = USerId;
            return entity;
        }

        public Users UserDeleteMap(Users entity, int UserId)
        {
            entity.IsActive = false;
            entity.ModifyOn = DateTime.UtcNow;
            entity.ModifyBy = UserId;
            return entity;
        }
    }
}