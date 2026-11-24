using GymSystem.Application.Interfaces;
using GymSystem.Infrastructure.Repositories.AccountRepo;
using GymSystem.Infrastructure.Repositories.MemberRepo;
using GymSystem.Infrastructure.Repositories.PermissionRepo;
using GymSystem.Infrastructure.Repositories.PlanRepo;
using GymSystem.Infrastructure.Repositories.RoleRepo;
using GymSystem.Infrastructure.Repositories.SubscriptionRepo;
using GymSystem.Infrastructure.Repositories.TrainerRepo;
using GymSystem.Infrastructure.Repositories.UserRepo;

namespace GymSystem.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        IMemberRepository MemberRepo { get; }
        ISubscriptionRepository SubscriptionRepo { get; }
        IPlanRepository PlanRepo { get; }
        IUserRepository UserRepo { get; }
        IRoleRepository RoleRepo { get; }
        IPermissionRepository PermissionRepo { get; }
        IAccountRepository AccountRepo { get; }
        IPaymentRepository PaymentRepo { get; }
        ITrainerRepository TrainerRepo { get; }
        IAttendanceRepository AttendanceRepo { get; }

        void Save();
    }
}