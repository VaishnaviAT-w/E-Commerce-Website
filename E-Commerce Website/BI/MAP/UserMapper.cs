using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.Entity;

namespace E_Commerce_Website.BI.Mapper
{
    public class UserMapper
    {
        //ADD ENTITY
        public Users AddUserMapper(UsersDto dto)
        {
            return new Users
            {
                Fullname = dto.FullName,
                Email = dto.Emails,
                PasswordHash = dto.Password,
                Mobile = dto.MobileNo,
                Role = dto.Role,
                IsActive = dto.IsActive,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = dto.Id
            };
        }

        // UPDATE ENTITY
        public void UpdateUserMapper(Users user, UsersDto dto)
        {
            user.Fullname = dto.FullName;
            user.Email = dto.Emails;
            user.PasswordHash = dto.Password;
            user.Mobile = dto.MobileNo;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;
            user.ModifyOn = DateTime.UtcNow;
            user.ModifyBy = dto.Id;
        }

        // ENTITY - DTO (GET)
        public static UsersDto MaptoDto(Users user)
        {
            return new UsersDto
            {
                Id = user.UserId,
                FullName = user.Fullname,
                Emails = user.Email,
                MobileNo = user.Mobile,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        // DELETE ENTITY
        public static void DeleteMapper(Users user)
        {
            user.IsActive = false;
            user.ModifyOn = DateTime.UtcNow;
        }
    }
}