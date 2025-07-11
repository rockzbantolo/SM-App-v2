using Microsoft.EntityFrameworkCore;
using ServiceDesk.Core.Models;
using ServiceDesk.Core.Interfaces;
using ServiceDesk.Infrastructure.Data;


namespace ServiceDesk.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _db;

        public UserRepository(AppDataContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Users
               .Include(u => u.UserProfile)
               .Include(u => u.UserRoles)
                 .ThenInclude(ur => ur.Role)
                 .ThenInclude(r => r.RolePermissions)
                 .ThenInclude(rp => rp.Permission)
                 .ThenInclude(p => p.Module)
               .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByUserIdAsync(int userId)
        {
            return await _db.Users
                 .Include(u => u.UserProfile)
                 .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
                   .ThenInclude(r => r.RolePermissions)
                   .ThenInclude(rp => rp.Permission)
                   .ThenInclude(p => p.Module)
                 .FirstOrDefaultAsync(u => u.UserID == userId);
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.PasswordHash = newPasswordHash;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _db.Users
                .Include(u => u.UserProfile) //navigation property
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(User user)
        {
            _db.Users.Add(user);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(User updatedUser)
        {
            var existingUser = await _db.Users.FindAsync(updatedUser.UserID);

            if (existingUser == null) return false;

            // Only update fields that are not null
            if (!string.IsNullOrEmpty(updatedUser.Email))
                existingUser.Email = updatedUser.Email;

            // Always update timestamps or necessary fields
            existingUser.UpdatedAt = DateTime.UtcNow;

            return await _db.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteAsync(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            _db.Users.Remove(user);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetActiveStatusAsync(int userId, bool isActive)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = isActive;
            user.UpdatedAt = DateTime.UtcNow;

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _db.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }


        public async Task<bool> AssignRoleAsync(int userId, int roleId)
        {
            var exists = await _db.UserRoles
                .AnyAsync(ur => ur.UserID == userId && ur.RoleID == roleId);

            if (exists) return true; // Already assigned

            var userRole = new UserRole
            {
                UserID = userId,
                RoleID = roleId,
                AssignedAt = DateTime.UtcNow
            };

            _db.UserRoles.Add(userRole);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRoleAsync(int userId, int roleId)
        {
            var userRole = await _db.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserID == userId && ur.RoleID == roleId);

            if (userRole == null) return false;

            _db.UserRoles.Remove(userRole);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Role>> GetUserRolesAsync(int userId)
        {
            return await _db.UserRoles
                .Where(ur => ur.UserID == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<List<UserPermInfo>> GetUserPermissionsAsync(int userId)
        {
            var permissions = await _db.UserRoles
                .Where(ur => ur.UserID == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => new UserPermInfo
                {
                    PermissionID = rp.PermissionID,
                    ModuleID = rp.Permission.ModuleID,
                    ModuleName = rp.Permission.Module.ModuleName,
                    Action = rp.Permission.Action,
                    Description = rp.Permission.Description
                })
                .Distinct()
                .ToListAsync();

            return permissions;
        }

        public async Task LogActivityAsync(UserActivity activity)
        {
            if (activity == null) return;

            activity.ActivityTimestamp = DateTime.UtcNow;

            _db.UserActivities.Add(activity);
            await _db.SaveChangesAsync();
        }

    }
}
