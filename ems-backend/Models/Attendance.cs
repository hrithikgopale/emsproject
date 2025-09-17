using System.ComponentModel.DataAnnotations;

namespace ems_backend.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        public Employee? Employee { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(20)]
        public string? Status { get; set; }
    }
}
