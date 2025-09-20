using ems_backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Reports/EmployeeSummary
        [HttpGet("EmployeeSummary")]
        public async Task<IActionResult> GetEmployeeSummary()
        {
            var totalEmployees = await _context.Employees.CountAsync();
            var avgSalary = await _context.Employees.AverageAsync(e => e.Salary);
            var deptWise = await _context.Employees
                .GroupBy(e => e.DepartmentID)
                .Select(g => new
                {
                    DepartmentID = g.Key,
                    Count = g.Count(),
                    AvgSalary = g.Average(e => e.Salary)
                }).ToListAsync();

            return Ok(new
            {
                TotalEmployees = totalEmployees,
                AverageSalary = avgSalary,
                DepartmentWise = deptWise
            });
        }

        // GET: api/Reports/AttendanceSummary/2025-09
        [HttpGet("AttendanceSummary/{yearMonth}")]
        public async Task<IActionResult> GetAttendanceSummary(string yearMonth)
        {
            if (!DateTime.TryParse(yearMonth + "-01", out var startDate))
                return BadRequest("Invalid yearMonth format. Use yyyy-MM");

            var endDate = startDate.AddMonths(1);

            var summary = await _context.Attendances
                .Where(a => a.Date >= startDate && a.Date < endDate)
                .GroupBy(a => a.EmployeeID)
                .Select(g => new
                {
                    EmployeeID = g.Key,
                    DaysPresent = g.Count(a => a.Status == "Present"),
                    DaysAbsent = g.Count(a => a.Status != "Present")
                }).ToListAsync();

            return Ok(summary);
        }
    }
}
