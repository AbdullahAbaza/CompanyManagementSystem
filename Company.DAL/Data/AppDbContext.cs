using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Company.DAL.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Server = DESKTOP-7RK5GNH\\PROJECTMODELS; Database = MVCApplication; Trusted_Connection = True; "); /*MultipleActiveResultSets = True */


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cal Configuration Classes

            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfiguration());

            // Better Way Based On Reflection: 
            //  --> Get All Classes Implementing IEntityTypeComfiguration<TEnity> and       apply is configuration  

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

    }
}
