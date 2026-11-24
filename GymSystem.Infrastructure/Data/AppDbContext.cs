using Microsoft.EntityFrameworkCore;
using GymSystem.Domain.Models;
using GymSystem.Domain.Entities;

namespace GymSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=MSI\\MSSQLSERVER3;Database=GymSystem;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        // جداول نظام الجيم الأساسية
        public DbSet<Member> Members { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Plan> Plans { get; set; }

        // جداول الصلاحيات والمستخدمين (نفس الستايل المعتمد)
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Attendance> Attendances { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ربط جدول الصلاحيات بالأدوار (PermissionRoles أو RolePermissions حسب الكيان عندك)
            modelBuilder.Entity<RolePermission>()
                .HasKey(pr => new
                {
                    pr.RoleId,
                    pr.PermissionId
                });

            // ربط جدول المستخدمين بالأدوار (UserRoles)
            modelBuilder.Entity<UserRole>()
                .HasKey(ru => new
                {
                    ru.RoleId,
                    ru.UserId
                });
        }
    }
}