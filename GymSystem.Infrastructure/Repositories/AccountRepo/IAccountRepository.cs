using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Infrastructure.Repositories.AccountRepo;

public interface IAccountRepository : IRepository<User>
{
    User? GetByUsername(string username);
    void Save();
}