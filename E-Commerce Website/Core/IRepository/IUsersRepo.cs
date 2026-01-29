using E_Commerce_Website.Core.Entity;

namespace E_Commerce_Website.Core.Contract.IRepository
{
    public interface IUsersRepo
    {
        Task<int> AddUsers(Users user);
        Task<int> UpdateUsers(Users user);
        IQueryable<Users> GetAllUsers();
        Task<Users?> GetUsersById(int id);
        Task<Users?> GetByEmail(string email);
    }
}

 