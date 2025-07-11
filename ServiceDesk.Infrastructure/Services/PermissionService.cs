using Microsoft.EntityFrameworkCore;
using ServiceDesk.Core.Models;
using ServiceDesk.Infrastructure.Data;


namespace ServiceDesk.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDataContext _db;

        public PermissionService(AppDataContext db)
        {
            _db = db;
        }

        public async Task<bool> HasPermissionAsync(int userId, string moduleName, string action)
        {
            // Check if SuperAdmin
            bool isSuperAdmin = await _db.UserRoles
                .AnyAsync(ur => ur.UserID == userId && ur.Role.RoleName == "SuperAdmin");

            if (isSuperAdmin) return true;

            // Check if any permission matches
            bool hasPermission = await _db.UserRoles
                .Where(ur => ur.UserID == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .AnyAsync(rp =>
                    rp.Permission.Module.ModuleName == moduleName &&
                    rp.Permission.Action == action);

            return hasPermission;
        }

        public async Task<List<Permission>> GetUserPermissionsAsync(int userId)
        {
            return await _db.UserRoles
                .Where(ur => ur.UserID == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission)
                .Include(p => p.Module)
                .Where(p => p.IsActive)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Module>> GetUserModulesAsync(int userId)
        {
            bool isSuperAdmin = await _db.UserRoles
                .AnyAsync(ur => ur.UserID == userId && ur.Role.RoleName == "SuperAdmin");

            if (isSuperAdmin)
            {
                return await _db.Modules
                    .Where(m => m.IsActive)
                    .OrderBy(m => m.SortOrder)
                    .ToListAsync();
            }

            return await _db.UserRoles
                .Where(ur => ur.UserID == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Module)
                .Where(m => m.IsActive)
                .Distinct()
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _db.Permissions
                .Include(p => p.Module)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<List<Module>> GetAllModulesAsync()
        {
            return await _db.Modules
                .Where(m => m.IsActive)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }
    }

}
