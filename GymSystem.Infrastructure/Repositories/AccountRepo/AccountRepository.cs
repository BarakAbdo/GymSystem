using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Data;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Infrastructure.Repositories.AccountRepo;

public class AccountRepository : Repository<User>, IAccountRepository
{
    private readonly AppDbContext _db;

    public AccountRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public User? GetByUsername(string username)
    {
        return _db.Users.FirstOrDefault(u => u.Username == username);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}