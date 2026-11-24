using GymSystem.Application.Dtos;
using GymSystem.Application.Dtos.UsersDtos;
using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = _unitOfWork.UserRepo.GetAll();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            UID = u.UID,
            Name = u.Name,
            Email = u.Email,
            Username = u.Username
        }).ToList();
    }

    public UserDto GetUserById(int id)
    {
        var u = _unitOfWork.UserRepo.GetById(id);
        return new UserDto
        {
            Id = u.Id,
            UID = u.UID,
            Name = u.Name,
            Email = u.Email,
            Username = u.Username
        };
    }

    public UserDto GetUserByUId(string uid)
    {
        var u = _unitOfWork.UserRepo.GetByUId(uid);
        return new UserDto
        {
            Id = u.Id,
            UID = u.UID,
            Name = u.Name,
            Email = u.Email,
            Username = u.Username
        };
    }

    public void AddUser(CreateUserDto userDto)
    {
        var user = new User
        {
            UID = Guid.NewGuid().ToString(),
            Name = userDto.Name,
            Email = userDto.Email,
            Username = userDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
        };

        _unitOfWork.UserRepo.Add(user);
        _unitOfWork.Save();
    }

    public void UpdateUser(UpdateUserDto userDto)
    {
        var oldUser = _unitOfWork.UserRepo.GetById(userDto.Id);
        if (oldUser == null) return;

        if (string.IsNullOrEmpty(userDto.UID))
        {
            userDto.UID = Guid.NewGuid().ToString();
        }

        oldUser.UID = userDto.UID;
        oldUser.Name = userDto.Name;
        oldUser.Email = userDto.Email;
        oldUser.Username = userDto.Username;

        if (!string.IsNullOrEmpty(userDto.Password))
        {
            oldUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        }

        _unitOfWork.UserRepo.Update(oldUser);
        _unitOfWork.Save();
    }

    public void DeleteUser(UserDto userDto)
    {
        var oldUser = _unitOfWork.UserRepo.GetByUId(userDto.UID);
        if (oldUser != null)
        {
            _unitOfWork.UserRepo.Delete(oldUser);
            _unitOfWork.Save();
        }
    }

    public IEnumerable<Role> GetAllRoles()
    {
        return _unitOfWork.UserRepo.Roles.ToList();
    }

    public IEnumerable<int> GetUserRoleIds(int userId)
    {
        return _unitOfWork.UserRepo.RoleUsers
            .Where(x => x.UserId == userId)
            .Select(x => (int)x.RoleId)
            .ToList();
    }

    public void UpdateUserRoles(int userId, List<int> roleIds)
    {
        _unitOfWork.UserRepo.UpdateUserRoles(userId, roleIds);
        _unitOfWork.Save();
    }

    public User? Login(LoginDto loginDto)
    {
        var user = _unitOfWork.UserRepo.GetByUsername(loginDto.Username);
        if (user == null || string.IsNullOrEmpty(user.PasswordHash)) return null;

        bool userFound = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        return userFound ? user : null;
    }

    public User? Register(RegisterDto registerDto)
    {
        var existingUsername = _unitOfWork.UserRepo.GetByUsername(registerDto.Username);
        var existingEmail = _unitOfWork.UserRepo.GetByEmail(registerDto.Email);

        if (existingUsername != null || existingEmail != null) return null;

        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Username = registerDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        _unitOfWork.UserRepo.Add(user);
        _unitOfWork.Save();

        return user;
    }
}
