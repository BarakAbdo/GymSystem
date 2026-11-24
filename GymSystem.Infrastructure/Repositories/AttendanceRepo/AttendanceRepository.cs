using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Threading.Tasks;


namespace GymSystem.Infrastructure.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        private readonly AppDbContext _db;

        public AttendanceRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Attendance?> GetByUid(string uid)
        {
            return await _db.Attendances.FirstOrDefaultAsync(a => a.UID == uid);
        }
    }
}