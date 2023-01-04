using Microsoft.EntityFrameworkCore;
using MvcCrudApplication.Models.Domain;

namespace MvcCrudApplication.Data
{
    public class MVCProjectDbContext : DbContext
    {
        public MVCProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }


    }

}
  