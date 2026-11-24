using GymSystem.Application.Dtos.PermissionsDtos;

public interface IPermissionService
{
    IEnumerable<PermissionDto> GetAllPermissions();
    PermissionDto GetPermissionById(int id);
    PermissionDto GetPermissionByUId(string uid);

    void AddPermission(CreatePermissionDto permissionDto);
    void UpdatePermission(UpdatePermissionDto permissionDto);
    void DeletePermission(PermissionDto permissionDto);
}