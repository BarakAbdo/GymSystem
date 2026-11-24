using GymSystem.Application.Services.Base;
using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddUser(User user)
    {
        // تشفير كلمة المرور قبل الحفظ
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        _unitOfWork.AccountRepo.Add(user);
        _unitOfWork.Save();
    }

    public User? GetByUsername(string username)
    {
        return _unitOfWork.AccountRepo.GetByUsername(username);
    }

    public User? Login(string username, string password)
    {
        var user = _unitOfWork.AccountRepo.GetByUsername(username);

        if (user != null && !string.IsNullOrEmpty(user.PasswordHash) && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return user;
        }

        return null;
    }
}