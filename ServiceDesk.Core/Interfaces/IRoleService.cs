using ServiceDesk.Core.Models;

namespace ServiceDesk.Core.Services
{
    public interface IRoleService
    {
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<Role?> GetRoleByIdAsync(int roleId);
        Task<List<Role>> GetAllRolesAsync();
        Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId);
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId);
        Task<List<Permission>> GetRolePermissionsAsync(int roleId);
    }
}
