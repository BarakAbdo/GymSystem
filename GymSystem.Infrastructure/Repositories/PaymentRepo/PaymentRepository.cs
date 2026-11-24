using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore; // مسار الـ DbContext عندك

namespace GymSystem.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _db;

        public PaymentRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public Payment? GetByUid(string uid)
        {
            return _db.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Member)
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Plan)
                .FirstOrDefault(p => p.UID == uid);
        }
    }
}