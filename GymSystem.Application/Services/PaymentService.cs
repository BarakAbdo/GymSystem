using System.Collections.Generic;
using System.Linq;
using GymSystem.Application.Dtos.PaymentDtos;
using GymSystem.Application.Services;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PaymentDto> GetAll()
        {
            var payments = _unitOfWork.PaymentRepo.GetAll();

            // جلب البيانات الأساسية باستخدام القواميس لتجنب مشاكل الـ Include والـ Joins
            var subscriptions = _unitOfWork.SubscriptionRepo.GetAll().ToDictionary(s => s.Id);
            var members = _unitOfWork.MemberRepo.GetAll().ToDictionary(m => m.Id);
            var plans = _unitOfWork.PlanRepo.GetAll().ToDictionary(p => p.Id);

            return payments.Select(p =>
            {
                string memberName = string.Empty;
                string planName = string.Empty;

                if (subscriptions.ContainsKey(p.SubscriptionId))
                {
                    var sub = subscriptions[p.SubscriptionId];
                    if (members.ContainsKey(sub.MemberId))
                        memberName = members[sub.MemberId].Name;
                    if (plans.ContainsKey(sub.PlanId))
                        planName = plans[sub.PlanId].Name;
                }

                return new PaymentDto
                {
                    Id = p.Id,
                    UID = p.UID,
                    SubscriptionId = p.SubscriptionId,
                    MemberName = memberName,
                    PlanName = planName,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    Status = p.Status,
                    ReferenceNumber = p.ReferenceNumber,
                };
            }).OrderByDescending(p => p.PaymentDate).ToList();
        }

        public PaymentDto? GetById(int id)
        {
            var p = _unitOfWork.PaymentRepo.GetById(id);
            if (p == null) return null;

            var sub = _unitOfWork.SubscriptionRepo.GetById(p.SubscriptionId);
            string memberName = string.Empty;
            string planName = string.Empty;

            if (sub != null)
            {
                memberName = _unitOfWork.MemberRepo.GetById(sub.MemberId)?.Name ?? string.Empty;
                planName = _unitOfWork.PlanRepo.GetById(sub.PlanId)?.Name ?? string.Empty;
            }

            return new PaymentDto
            {
                Id = p.Id,
                UID = p.UID,
                SubscriptionId = p.SubscriptionId,
                MemberName = memberName,
                PlanName = planName,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod,
                Status = p.Status,
                ReferenceNumber = p.ReferenceNumber,
            };
        }

        public PaymentDto? GetByUid(string uid)
        {
            var p = _unitOfWork.PaymentRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (p == null) return null;

            var sub = _unitOfWork.SubscriptionRepo.GetById(p.SubscriptionId);
            string memberName = string.Empty;
            string planName = string.Empty;

            if (sub != null)
            {
                memberName = _unitOfWork.MemberRepo.GetById(sub.MemberId)?.Name ?? string.Empty;
                planName = _unitOfWork.PlanRepo.GetById(sub.PlanId)?.Name ?? string.Empty;
            }

            return new PaymentDto
            {
                Id = p.Id,
                UID = p.UID,
                SubscriptionId = p.SubscriptionId,
                MemberName = memberName,
                PlanName = planName,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod,
                Status = p.Status,
                ReferenceNumber = p.ReferenceNumber,
            };
        }

        public void Add(CreatePaymentDto dto)
        {
            var payment = new Payment
            {
                UID = string.IsNullOrWhiteSpace(dto.UID) ? System.Guid.NewGuid().ToString() : dto.UID,
                SubscriptionId = dto.SubscriptionId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
                PaymentMethod = dto.PaymentMethod,
                Status = dto.Status,
                ReferenceNumber = dto.ReferenceNumber,
            };

            _unitOfWork.PaymentRepo.Add(payment);
            _unitOfWork.Save();
        }

        public void Update(string uid, UpdatePaymentDto dto)
        {
            var payment = _unitOfWork.PaymentRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (payment == null) return;

            payment.SubscriptionId = dto.SubscriptionId;
            payment.Amount = dto.Amount;
            payment.PaymentDate = dto.PaymentDate;
            payment.PaymentMethod = dto.PaymentMethod;
            payment.Status = dto.Status;
            payment.ReferenceNumber = dto.ReferenceNumber;

            _unitOfWork.PaymentRepo.Update(payment);
            _unitOfWork.Save();
        }

        public void Delete(string uid)
        {
            var payment = _unitOfWork.PaymentRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (payment == null) return;

            _unitOfWork.PaymentRepo.Delete(payment);
            _unitOfWork.Save();
        }
    }
}