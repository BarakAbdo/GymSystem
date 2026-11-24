using GymSystem.Application.Dtos;
using GymSystem.Application.Dtos.UsersDtos;
using GymSystem.Domain.Models;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    UserDto GetUserById(int id);
    UserDto GetUserByUId(string uid);

    void AddUser(CreateUserDto userDto);
    void UpdateUser(UpdateUserDto userDto);
    void DeleteUser(UserDto userDto);

    IEnumerable<Role> GetAllRoles();
    IEnumerable<int> GetUserRoleIds(int userId);
    void UpdateUserRoles(int userId, List<int> roleIds);

    User? Login(LoginDto loginDto);
    User? Register(RegisterDto registerDto);
}