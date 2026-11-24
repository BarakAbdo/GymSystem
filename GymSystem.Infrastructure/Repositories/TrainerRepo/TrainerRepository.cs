using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;
using GymSystem.Infrastructure.Repositories.TrainerRepo;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymSystem.Infrastructure.Repositories
{
    public class TrainerRepository : Repository<Trainer>, ITrainerRepository
    {
        private readonly AppDbContext _db;

        public TrainerRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Trainer?> GetByUid(string uid)
        {
            return await _db.Trainers.FirstOrDefaultAsync(t => t.UID == uid);
        }
    }
}
