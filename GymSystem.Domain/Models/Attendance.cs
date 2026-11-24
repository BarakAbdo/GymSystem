using System;

namespace GymSystem.Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
        public int MemberId { get; set; } // أو ربطه بالعضو
        public DateTime CheckInTime { get; set; } = DateTime.Now;
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; } = "Present"; // Present, Late, Absent
        public string? Notes { get; set; }
    }
}