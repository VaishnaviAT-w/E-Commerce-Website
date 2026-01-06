using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Entity;

namespace E_Commerce_Website.BI.Mapper
{
    public class UserMapper
    {
        // ADD
        public Users UserSaveMap(UsersRequest request)
        {
            return new Users
            {
                Fullname = request.FullName?.Trim(),
                Email = request.Email,
                PasswordHash = request.Password,
                Mobile = request.MobileNo,
                Role = request.Role,
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            };
        }

        // UPDATE
        public Users UserUpdateMap(Users entity, UsersRequest request)
        {
            entity.Fullname = request.FullName?.Trim();
            entity.Email = request.Email;
            entity.Mobile = request.MobileNo;
            entity.Role = request.Role;
            entity.IsActive = request.IsActive;
            entity.ModifyOn = DateTime.UtcNow;
            return entity;
        }

        // DELETE
        public Users UserDeleteMap(Users entity)
        {
            entity.IsActive = false;
            entity.ModifyOn = DateTime.UtcNow;
            return entity;
        }
    }
}