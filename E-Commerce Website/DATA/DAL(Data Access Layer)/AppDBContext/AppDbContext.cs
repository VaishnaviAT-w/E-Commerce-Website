using E_Commerce_Website.Core.Enitities;
using E_Commerce_Website.Core.Entity;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce_Website.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }
}
 