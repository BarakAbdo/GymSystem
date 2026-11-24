using GymSystem.Application.Dtos.DashboardDtos;
using GymSystem.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DashboardDto GetDashboardData()
        {
            var members = _unitOfWork.MemberRepo.GetAll().ToList();
            var subscriptions = _unitOfWork.SubscriptionRepo.GetAll().ToList();
            var trainers = _unitOfWork.TrainerRepo.GetAll().ToList();
            var payments = _unitOfWork.PaymentRepo.GetAll().ToList();
            var attendances = _unitOfWork.AttendanceRepo.GetAll().ToList();

            var today = DateTime.Today;

            var dto = new DashboardDto
            {
                TotalMembers = members.Count,
                ActiveSubscriptions = subscriptions.Count(s => s.EndDate >= today),
                TotalTrainers = trainers.Count(t => t.IsActive),
                TodayAttendances = attendances.Count(a => a.CheckInTime.Date == today),
                TotalRevenue = payments.Where(p => p.Status == "Paid").Sum(p => p.Amount),
                ExpiredSubscriptions = subscriptions.Count(s => s.EndDate < today),
                TodayRevenue = payments.Where(p => p.Status == "Paid" && p.PaymentDate.Date == today).Sum(p => p.Amount),
                LastUpdated = DateTime.Now
            };

            return dto;
        }
    }
}

