using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ems_backend.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public DateTime DateOfJoining { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }

        // Navigation
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
