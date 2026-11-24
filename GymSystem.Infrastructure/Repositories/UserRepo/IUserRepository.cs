using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Infrastructure.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }
        IEnumerable<UserRole> RoleUsers { get; }

        User? GetByUsername(string username);
        User? GetByEmail(string email);
        void Add(User user);
        User GetByUId(string uid);
        void UpdateUserRoles(int userId, List<int> selectedRoleIds);
    }
}