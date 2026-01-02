using System.Collections.Generic;
using CRUD.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DATA.DAL_Data_Access_Layer_.AppDBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<StudentDetails> studentdetails { get; set; }



    }
}


