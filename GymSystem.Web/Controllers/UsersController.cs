using GymSystem.Application.Dtos;
using GymSystem.Application.Dtos.UsersDtos;
using GymSystem.Application.Services.Base;
using GymSystem.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                _userService.AddUser(userDto);
                return RedirectToAction("Index");
            }
            return View(userDto);
        }

        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var user = _userService.GetUserByUId(uid);
            if (user == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateUserDto
            {
                Id = user.Id,
                UID = user.UID,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username
            };

            return View(updateDto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateUserDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userService.GetUserById(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var user = _userService.GetUserByUId(uid);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(UserDto user)
        {
            var oldUser = _userService.GetUserByUId(user.UID);
            if (oldUser == null)
            {
                return NotFound();
            }
            _userService.DeleteUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageRoles(string uid)
        {
            var user = _userService.GetUserByUId(uid);
            if (user == null)
            {
                return NotFound();
            }

            var roles = _userService.GetAllRoles();
            var userRoleIds = _userService.GetUserRoleIds(user.Id);

            var model = new UserRolesVM
            {
                UserId = user.Id,
                UserUid = user.UID,
                UserName = user.Username,
                Roles = roles.Select(role => new RoleCheckVM
                {
                    RoleId = role.Id,
                    RoleUid = role.UID,
                    RoleName = role.Name,
                    IsSelected = userRoleIds.Contains(role.Id)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageRoles(UserRolesVM model)
        {
            var user = _userService.GetUserById(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var selectedRoleIds = model.Roles?
                .Where(r => r.IsSelected)
                .Select(r => r.RoleId)
                .ToList() ?? new List<int>();

            _userService.UpdateUserRoles(model.UserId, selectedRoleIds);

            return RedirectToAction("Index");
        }
    }
}