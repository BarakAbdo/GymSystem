using System;

namespace GymSystem.Application.Dtos.AttendanceDtos
{
    public class CreateAttendanceDto
    {
        public int MemberId { get; set; }
        public DateTime CheckInTime { get; set; } = DateTime.Now;
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; } = "Present";
        public string? Notes { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdateAttendanceDto : CreateAttendanceDto
    {
        public int Id { get; set; }
    }

    public class AttendanceDto : UpdateAttendanceDto
    {
        public string MemberName { get; set; } = string.Empty;
    }
}