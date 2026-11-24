using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymSystem.Infrastructure.Repositories.SubscriptionRepo
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private readonly AppDbContext _db;

        public SubscriptionRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Subscription?> GetByUid(string uid)
        {
            return await _db.Subscriptions
                .Include(s => s.Member)
                .FirstOrDefaultAsync(s => s.UID.ToString() == uid);
        }
    }
}