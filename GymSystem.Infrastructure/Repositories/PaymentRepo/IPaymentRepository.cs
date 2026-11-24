using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Application.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Payment? GetByUid(string uid);
    }
}