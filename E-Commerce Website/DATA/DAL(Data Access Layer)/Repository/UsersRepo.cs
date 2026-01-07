using E_Commerce_Website.Core.Contract.IRepository;
using E_Commerce_Website.Core.Entity;
using E_Commerce_Website.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Website.DATA.DAL_Data_Access_Layer_.Repository
{
    public class UserRepo : IUsersRepo
    {
        private readonly ApplicationDBContext _Context;

        public UserRepo(ApplicationDBContext context)
        {
            _Context = context;
        }

        public async Task<int> AddUsers(Users user)
        {
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<int> UpdateUsers(Users user)
        {
            _Context.Users.Update(user);
            await _Context.SaveChangesAsync();
            return user.UserId;
        }

        public IQueryable<Users> GetAllUsers()
        {
            return _Context.Users.Where(x=>x.IsActive);
        }
         
        public async Task<Users?> GetUsersById(int id)
        {
            return await _Context.Users
                .FirstOrDefaultAsync(x => x.UserId == id && x.IsActive == true);
        }
    }
}
  