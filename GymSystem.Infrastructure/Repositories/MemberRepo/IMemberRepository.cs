using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Infrastructure.Repositories.MemberRepo
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member?> GetByUid(string uid);
    }
}