using GymSystem.Application.Dtos.AttendanceDtos;
using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var attendances = _attendanceService.GetAll();
            return Ok(attendances);
        }

        [HttpGet("{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var attendance = _attendanceService.GetByUid(uid);
            if (attendance == null)
            {
                return NotFound(new { message = "Attendance record not found" });
            }

            return Ok(attendance);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateAttendanceDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid attendance data" });
            }

            _attendanceService.Add(dto);
            return Ok(new { message = "Attendance added successfully" });
        }

        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] UpdateAttendanceDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid attendance data" });
            }

            var existing = _attendanceService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Attendance record not found" });
            }

            _attendanceService.Update(uid, dto);
            return Ok(new { message = "Attendance updated successfully" });
        }

        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            var existing = _attendanceService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Attendance record not found" });
            }

            _attendanceService.Delete(uid);
            return Ok(new { message = "Attendance deleted successfully" });
        }
    }
}