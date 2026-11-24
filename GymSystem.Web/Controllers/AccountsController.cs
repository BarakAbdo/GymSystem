using GymSystem.Application.Services.Base;
using GymSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers;

public class AccountsController : Controller
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _accountService.Login(username, password);

        if (user != null)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Invalid username or password");
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string email, string password)
    {
        var user = new User
        {
            UID = Guid.NewGuid().ToString(),
            Username = username,
            Email = email,
            PasswordHash = password,
            IsActive = true
        };

        _accountService.AddUser(user);
        return RedirectToAction(nameof(Login));
    }
}