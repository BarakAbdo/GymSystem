using GymSystem.Application.Dtos.SubscriptionsDtos;
using GymSystem.Domain.Entities;
using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace GymSystem.Application.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SubscriptionDto> GetAll()
        {
            var subscriptions = _unitOfWork.SubscriptionRepo.GetAll();

            return subscriptions.Select(s => new SubscriptionDto
            {
                Id = s.Id,
                UID = s.UID,
                MemberId = s.MemberId,
                PlanId = s.PlanId,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                MemberName = _unitOfWork.MemberRepo.GetById(s.MemberId)?.Name ?? string.Empty,
                PlanName = _unitOfWork.PlanRepo.GetById(s.PlanId)?.Name ?? string.Empty,
                PlanCategory = _unitOfWork.PlanRepo.GetById(s.PlanId)?.Category ?? string.Empty,
                Price = _unitOfWork.PlanRepo.GetById(s.PlanId)?.Price ?? 0
            }).ToList();
        }

        public SubscriptionDto? GetById(int id)
        {
            var s = _unitOfWork.SubscriptionRepo.GetById(id);
            if (s == null) return null;

            var member = _unitOfWork.MemberRepo.GetById(s.MemberId);
            var plan = _unitOfWork.PlanRepo.GetById(s.PlanId);

            return new SubscriptionDto
            {
                Id = s.Id,
                UID = s.UID,
                MemberId = s.MemberId,
                PlanId = s.PlanId,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                MemberName = member?.Name ?? string.Empty,
                PlanName = plan?.Name ?? string.Empty,
                PlanCategory = plan?.Category ?? string.Empty,
                Price = plan?.Price ?? 0
            };
        }

        public SubscriptionDto? GetByUid(string uid)
        {
            var s = _unitOfWork.SubscriptionRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (s == null) return null;

            var member = _unitOfWork.MemberRepo.GetById(s.MemberId);
            var plan = _unitOfWork.PlanRepo.GetById(s.PlanId);

            return new SubscriptionDto
            {
                Id = s.Id,
                UID = s.UID,
                MemberId = s.MemberId,
                PlanId = s.PlanId,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                MemberName = member?.Name ?? string.Empty,
                PlanName = plan?.Name ?? string.Empty,
                PlanCategory = plan?.Category ?? string.Empty,
                Price = plan?.Price ?? 0
            };
        }

        public void Add(CreateSubscriptionDto dto)
        {
            var plan = _unitOfWork.PlanRepo.GetById(dto.PlanId);
            if (plan == null) return;

            var calculatedEndDate = dto.StartDate.AddDays(plan.DurationDays);

            var subscription = new Subscription
            {
                UID = string.IsNullOrWhiteSpace(dto.UID) ? System.Guid.NewGuid().ToString() : dto.UID,
                MemberId = dto.MemberId,
                PlanId = dto.PlanId,
                StartDate = dto.StartDate,
                EndDate = calculatedEndDate
            };

            // 1. حفظ الاشتراك
            _unitOfWork.SubscriptionRepo.Add(subscription);
            _unitOfWork.Save();

            // 2. توليد الدفعة المالية (Payment) تلقائياً فوراً
            var payment = new Payment
            {
                UID = System.Guid.NewGuid().ToString(),
                SubscriptionId = subscription.Id, // ربط الدفعة برقم الاشتراك الجديد
                Amount = plan.Price,             // جلب السعر مباشرة من الخطة
                PaymentDate = System.DateTime.Now,
                PaymentMethod = "Cash",          // طريقة دفع افتراضية
                Status = "Pending",              // حالة الدفعة مبدئياً معلقة
                ReferenceNumber = "REF-" + new System.Random().Next(10000, 99999),
                Notes = "Auto-generated upon subscription creation"
            };

            _unitOfWork.PaymentRepo.Add(payment);
            _unitOfWork.Save();
        }

        public void Update(string uid, UpdateSubscriptionDto dto)
        {
            var subscription = _unitOfWork.SubscriptionRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (subscription == null) return;

            var plan = _unitOfWork.PlanRepo.GetById(dto.PlanId);
            if (plan == null) return;

            subscription.MemberId = dto.MemberId;
            subscription.PlanId = dto.PlanId;
            subscription.StartDate = dto.StartDate;
            subscription.EndDate = dto.StartDate.AddDays(plan.DurationDays);

            _unitOfWork.SubscriptionRepo.Update(subscription);
            _unitOfWork.Save();
        }

        public void Delete(string uid)
        {
            var subscription = _unitOfWork.SubscriptionRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (subscription == null) return;

            _unitOfWork.SubscriptionRepo.Delete(subscription);
            _unitOfWork.Save();
        }
    }
}