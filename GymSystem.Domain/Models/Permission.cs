using GymSystem.Domain.Models;
namespace GymSystem.Domain.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } // مثال: Members_Create, Members_Delete

        public string UID { get; set; } = Guid.NewGuid().ToString();

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}