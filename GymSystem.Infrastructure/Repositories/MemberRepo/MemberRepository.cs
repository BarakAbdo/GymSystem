using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore; // تأكد من وجود الـ using هذا عشان FirstOrDefaultAsync

namespace GymSystem.Infrastructure.Repositories.MemberRepo
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private readonly AppDbContext _db;

        public MemberRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        // تطبيق دالة البحث بالـ UID
        public async Task<Member?> GetByUid(string uid)
        {
            return await _db.Members // استبدل Members باسم الـ DbSet لديك إن لم يكن مطابقاً
                .FirstOrDefaultAsync(m => m.UID == uid);
        }
    }
}