using GymSystem.Application.Interfaces;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.AccountRepo;
using GymSystem.Infrastructure.Repositories.MemberRepo;
using GymSystem.Infrastructure.Repositories.PermissionRepo;
using GymSystem.Infrastructure.Repositories.PlanRepo;
using GymSystem.Infrastructure.Repositories.RoleRepo;
using GymSystem.Infrastructure.Repositories.SubscriptionRepo;
using GymSystem.Infrastructure.Repositories.TrainerRepo;
using GymSystem.Infrastructure.Repositories.UserRepo;
using System.Security.Cryptography.X509Certificates;

namespace GymSystem.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;

            MemberRepo = new MemberRepository(_db);

            SubscriptionRepo = new SubscriptionRepository(_db);

            PlanRepo = new PlanRepository(_db);

            UserRepo = new UserRepository(_db);

            RoleRepo = new RoleRepository(_db);

            PermissionRepo = new PermissionRepository(_db);

            AccountRepo = new AccountRepository(_db);

            PaymentRepo = new PaymentRepository(_db);

            TrainerRepo = new TrainerRepository(_db);

            AttendanceRepo = new AttendanceRepository(_db);
        }

        public IMemberRepository MemberRepo { get; }
        public ISubscriptionRepository SubscriptionRepo { get; }

        public IPlanRepository PlanRepo { get; }
        public IUserRepository UserRepo { get; }
        public IRoleRepository RoleRepo { get; }

        public IPermissionRepository PermissionRepo { get; }

        public IAccountRepository AccountRepo { get; }

        public IPaymentRepository PaymentRepo { get; }

        public ITrainerRepository TrainerRepo { get; }  
        public IAttendanceRepository AttendanceRepo { get; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}