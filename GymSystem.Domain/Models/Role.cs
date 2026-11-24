using GymSystem.Domain.Models;
namespace GymSystem.Domain.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } // مثال: Admin, Receptionist, Trainer
        public string UID { get; set; } = Guid.NewGuid().ToString();

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}