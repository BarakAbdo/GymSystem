using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Infrastructure.Repositories.RoleRepo
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> Roles { get; }
        IEnumerable<Permission> Permissions { get; }
        IEnumerable<RolePermission> RolePermissions { get; }
        Role GetByUId(string uid);
        void UpdatePermissions(int roleId, List<int> permissionIds);
    }
}