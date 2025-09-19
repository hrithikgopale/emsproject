using ems_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ems_backend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    UserName = "admin",
                    PasswordHash = "admin123", // ⚠️ replace with hash later
                    Role = "Admin"
                },
                new User
                {
                    UserID = 2,
                    UserName = "hrmanager",
                    PasswordHash = "hr123",
                    Role = "HR"
                },
                new User
                {
                    UserID = 3,
                    UserName = "employee1",
                    PasswordHash = "emp123",
                    Role = "Employee"
                }
            );
        }


    }
}
