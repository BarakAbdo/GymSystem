using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymSystem.Infrastructure.Repositories.PlanRepo
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly AppDbContext _db;

        public PlanRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Plan?> GetByUid(string uid)
        {
            return await _db.Plans
                .FirstOrDefaultAsync(p => p.UID == uid);
        }
    }
}