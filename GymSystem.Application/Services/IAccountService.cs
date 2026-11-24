using GymSystem.Domain.Models;

namespace GymSystem.Application.Services.Base;

public interface IAccountService
{
    void AddUser(User user);
    User? GetByUsername(string username);
    User? Login(string username, string password);
}