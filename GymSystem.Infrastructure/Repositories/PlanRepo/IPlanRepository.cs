using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;
using System.Threading.Tasks;

namespace GymSystem.Infrastructure.Repositories.PlanRepo
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<Plan?> GetByUid(string uid);
    }
}