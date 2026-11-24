using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Infrastructure.Repositories.PermissionRepo
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IEnumerable<Permission> Permissions { get; }
        Permission GetByUId(string uid);
    }
}