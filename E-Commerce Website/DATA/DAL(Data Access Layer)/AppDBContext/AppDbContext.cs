using Microsoft.EntityFrameworkCore;
using E_Commerce_Website.Core.Entity;
using E_Commerce_Website.Core.Enitities;
namespace E_Commerce_Website.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
 