using Microsoft.EntityFrameworkCore;
using ServiceDesk.Core.Models;
using ServiceDesk.Infrastructure.Data;


namespace ServiceDesk.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDataContext _db;

        public RoleService(AppDataContext db)
        {
            _db = db;
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            try
            {
                role.CreatedAt = DateTime.UtcNow;
                _db.Roles.Add(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create role: {ex.Message}", ex);
            }
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            try
            {
                var existingRole = await _db.Roles.FindAsync(role.RoleID);
                if (existingRole == null)
                    throw new Exception("Role not found.");

                existingRole.RoleName = role.RoleName;
                existingRole.Description = role.Description;
                existingRole.IsActive = role.IsActive;

                _db.Roles.Update(existingRole);
                await _db.SaveChangesAsync();
                return existingRole;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update role: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            try
            {
                var role = await _db.Roles.FindAsync(roleId);
                if (role == null) return false;

                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete role: {ex.Message}", ex);
            }
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _db.Roles
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                        .ThenInclude(p => p.Module)
                .ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _db.Roles
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                        .ThenInclude(p => p.Module)
                .FirstOrDefaultAsync(r => r.RoleID == roleId);
        }

        public async Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId)
        {
            try
            {
                var alreadyAssigned = await _db.RolePermissions
                    .AnyAsync(rp => rp.RoleID == roleId && rp.PermissionID == permissionId);

                if (alreadyAssigned) return true;

                var rolePermission = new RolePermission
                {
                    RoleID = roleId,
                    PermissionID = permissionId,
                    AssignedAt = DateTime.UtcNow
                };

                _db.RolePermissions.Add(rolePermission);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to assign permission: {ex.Message}", ex);
            }
        }

        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            try
            {
                var rolePermission = await _db.RolePermissions
                    .FirstOrDefaultAsync(rp => rp.RoleID == roleId && rp.PermissionID == permissionId);

                if (rolePermission == null) return false;

                _db.RolePermissions.Remove(rolePermission);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to remove permission: {ex.Message}", ex);
            }
        }

        public async Task<List<Permission>> GetRolePermissionsAsync(int roleId)
        {
            return await _db.RolePermissions
                .Where(rp => rp.RoleID == roleId)
                .Select(rp => rp.Permission)
                .Include(p => p.Module)
                .ToListAsync();
        }
    }

}
