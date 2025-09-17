using System.ComponentModel.DataAnnotations;

namespace ems_backend.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required, MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        // Navigation
        public ICollection<Employee>? Employees { get; set; }
    }
}
