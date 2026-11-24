using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Repositories.Base;
using System.Threading.Tasks;

namespace GymSystem.Infrastructure.Repositories
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Task<Attendance?> GetByUid(string uid);
    }
}