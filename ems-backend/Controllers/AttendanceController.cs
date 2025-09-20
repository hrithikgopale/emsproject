using ems_backend.Data;
using ems_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ems_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class AttendanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttendanceController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Attendance
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MarkAttendance([FromBody] Attendance attendance)
        {
            if (attendance == null) return BadRequest();

            _context.Attendances.Add(attendance);
            var response = await _context.Attendances
           .Where(a => a.AttendanceID == attendance.AttendanceID)
           .Include(a => a.Employee)
           .ThenInclude(e => e.Department)
           .FirstOrDefaultAsync();
            await _context.SaveChangesAsync();

            return Ok(response);
        }

        // GET: api/Attendance/employee/1
        [HttpGet("employee/{employeeId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAttendanceByEmployee(int employeeId)
        {
            var records = await _context.Attendances
                .Where(a => a.EmployeeID == employeeId)
                .ToListAsync();

            return Ok(records);
        }

        // GET: api/Attendance/date/2025-09-19
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetAttendanceByDate(DateTime date)
        {
            var records = await _context.Attendances
                .Where(a => a.Date.Date == date.Date)
                .Include(a => a.Employee)
                .ToListAsync();

            return Ok(records);
        }
    }
}
