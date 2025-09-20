namespace ems_backend.Dtos.Employees
{
    public class EmployeeReadDto
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string? DepartmentName { get; set; }
      
    }
}
