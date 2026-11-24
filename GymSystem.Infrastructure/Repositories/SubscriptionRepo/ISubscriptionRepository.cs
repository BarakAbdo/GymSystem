using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;
using System.Threading.Tasks;

namespace GymSystem.Infrastructure.Repositories.SubscriptionRepo
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<Subscription?> GetByUid(string uid);
    }
}