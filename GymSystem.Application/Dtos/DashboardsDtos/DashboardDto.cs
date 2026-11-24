using System;
using System.Collections.Generic;
using GymSystem.Application.Dtos.AttendanceDtos;
using GymSystem.Application.Dtos.PaymentDtos;

namespace GymSystem.Application.Dtos.DashboardDtos
{
    
    public class DashboardSummaryDto
    {
        public int TotalMembers { get; set; }
        public int ActiveSubscriptions { get; set; }
        public int TotalTrainers { get; set; }
        public int TodayAttendances { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    
    public class DashboardStatsDto : DashboardSummaryDto
    {
        public int ExpiredSubscriptions { get; set; }
        public decimal TodayRevenue { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    
    public class DashboardDto : DashboardStatsDto
    {
        public IEnumerable<PaymentDto> RecentPayments { get; set; } = new List<PaymentDto>();
        public IEnumerable<AttendanceDto> RecentAttendances { get; set; } = new List<AttendanceDto>();
    }
}