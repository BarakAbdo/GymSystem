using GymSystem.Domain.Models;
namespace GymSystem.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // <-- أضفنا هذا الحقل الناقص هنا
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string UID { get; set; } = Guid.NewGuid().ToString();

        // العلاقة مع جدول الوسيط UserRoles
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}