using GymSystem.Application.Dtos.RolesDtos;
using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<RoleDto> GetAllRoles()
    {
        var roles = _unitOfWork.RoleRepo.GetAll();
        return roles.Select(r => new RoleDto
        {
            Id = r.Id,
            UID = r.UID,
            Name = r.Name
        }).ToList();
    }

    public RoleDto GetRoleById(int id)
    {
        var r = _unitOfWork.RoleRepo.GetById(id);
        return new RoleDto { Id = r.Id, UID = r.UID, Name = r.Name };
    }

    public RoleDto GetRoleByUId(string uid)
    {
        var r = _unitOfWork.RoleRepo.GetByUId(uid);
        return new RoleDto { Id = r.Id, UID = r.UID, Name = r.Name };
    }

    public void AddRole(CreateRoleDto roleDto)
    {
        var role = new Role { UID = Guid.NewGuid().ToString(), Name = roleDto.Name };
        _unitOfWork.RoleRepo.Add(role);
        _unitOfWork.Save();
    }

    public void UpdateRole(UpdateRoleDto roleDto)
    {
        if (string.IsNullOrEmpty(roleDto.UID))
        {
            roleDto.UID = Guid.NewGuid().ToString();
        }

        var role = new Role { Id = roleDto.Id, UID = roleDto.UID, Name = roleDto.Name };
        _unitOfWork.RoleRepo.Update(role);
        _unitOfWork.Save();
    }

    public void DeleteRole(RoleDto roleDto)
    {
        var oldRole = _unitOfWork.RoleRepo.GetByUId(roleDto.UID);
        if (oldRole != null)
        {
            _unitOfWork.RoleRepo.Delete(oldRole);
            _unitOfWork.Save();
        }
    }

    public IEnumerable<Permission> GetAllPermissions()
    {
        return _unitOfWork.RoleRepo.Permissions.ToList();
    }

    public IEnumerable<int> GetAssignedPermissionIds(int roleId)
    {
        return _unitOfWork.RoleRepo.RolePermissions
            .Where(pr => pr.RoleId == roleId)
            .Select(pr => pr.PermissionId)
            .ToList();
    }

    public void UpdateRolePermissions(int roleId, List<int> permissionIds)
    {
        _unitOfWork.RoleRepo.UpdatePermissions(roleId, permissionIds);
        _unitOfWork.Save();
    }
}
