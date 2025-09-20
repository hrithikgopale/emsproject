using ems_backend.Data;
using ems_backend.Dtos.Employees;
using ems_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        [Authorize(Roles = "Admin,HR,Employee")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeReadDto
                {
                    EmployeeID = e.EmployeeID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Salary = e.Salary,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : string.Empty,
                    DateOfJoining = e.DateOfJoining
                })
                .ToListAsync();

            return Ok(employees);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,HR,Employee")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var e = await _context.Employees
                .Include(d => d.Department)
                .FirstOrDefaultAsync(emp => emp.EmployeeID == id);

            if (e == null) return NotFound();

            var dto = new EmployeeReadDto
            {
                EmployeeID = e.EmployeeID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Salary = e.Salary,
                DepartmentName = e.Department != null ? e.Department.DepartmentName : string.Empty,
                DateOfJoining = e.DateOfJoining
            };

            return Ok(dto);
        }

        // POST: api/employees
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDto dto)
        {
            var emp = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Salary = dto.Salary,
                DepartmentID = dto.DepartmentID,
                DateOfJoining = dto.DateOfJoining
            };

            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Employee created successfully", employeeId = emp.EmployeeID });
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto dto)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.FirstName)) emp.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) emp.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(dto.Phone)) emp.Phone = dto.Phone;
            if (dto.Salary.HasValue) emp.Salary = dto.Salary.Value;
            if (dto.DepartmentID.HasValue) emp.DepartmentID = dto.DepartmentID.Value;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Employee updated successfully" });
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
