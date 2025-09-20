namespace ems_backend.Dtos.Employees
{
    public class EmployeeUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public decimal? Salary { get; set; }
        public int? DepartmentID { get; set; }
    }
}
