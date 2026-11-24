using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Infrastructure.Repositories.RoleRepo
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Role> _dbSet;

        public RoleRepository(AppDbContext db) : base(db)
        {
            _db = db;
            _dbSet = _db.Set<Role>();
        }

        public IEnumerable<Role> Roles => _db.Roles.ToList();
        public IEnumerable<Permission> Permissions => _db.Permissions.ToList();
        public IEnumerable<RolePermission> RolePermissions => _db.RolePermissions.ToList();

        

        public Role? GetByUId(string uid)
        {
            return _dbSet.FirstOrDefault(e => e.UID == uid);
        }

        public void UpdatePermissions(int roleId, List<int> permissionIds)
        {
            var oldPermissions = _db.RolePermissions.Where(pr => pr.RoleId == roleId).ToList();
            _db.RolePermissions.RemoveRange(oldPermissions);

            if (permissionIds != null && permissionIds.Any())
            {
                foreach (var permissionId in permissionIds)
                {
                    _db.RolePermissions.Add(new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    });
                }
            }
        }
    }
}