using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSystem.Infrastructure.Repositories.TrainerRepo
{
    public interface ITrainerRepository : IRepository<Trainer>
    {
        Task<Trainer?> GetByUid(string uid);
    }
}