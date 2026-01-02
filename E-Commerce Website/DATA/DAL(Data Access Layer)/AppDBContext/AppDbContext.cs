using Microsoft.EntityFrameworkCore;
using E_Commerce_Website.Core.Entity;
namespace E_Commerce_Website.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
    }
}
