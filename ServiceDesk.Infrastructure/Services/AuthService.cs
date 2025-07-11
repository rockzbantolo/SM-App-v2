using Microsoft.EntityFrameworkCore;
using ServiceDesk.Core.Interfaces;
using ServiceDesk.Core.Models;
using ServiceDesk.Core.Shared;
using ServiceDesk.Infrastructure.Data;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ServiceDesk.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserSession?> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _userRepo.GetByUsernameAsync(username);
                if (user == null)
                    return null;

                //string inputPasswordHash = ComputeSha256Hash(password);
                
                if (user.PasswordHash != password)
                    return null;

                var session = BuildUserSession(user);
                    session.LoginTime = DateTime.UtcNow;

                SessionManager.SetCurrentUser(session); // Optional: auto-set session

                return session;
            }
            catch (Exception ex)
            {
                Logger.Error("Login failed", ex);
                throw new Exception($"Login failed: {ex.Message}", ex);
            }
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            SessionManager.ClearCurrentUser();
            return await Task.FromResult(true);
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _userRepo.GetByUserIdAsync(userId);
                if (user == null)
                    return false;

                string currentHash = ComputeSha256Hash(currentPassword);
                if (user.PasswordHash != currentHash)
                    return false;

                string newHash = ComputeSha256Hash(newPassword);
                return await _userRepo.UpdatePasswordAsync(userId, newHash);
            }
            catch (Exception ex)
            {
                Logger.Error("Change password failed", ex);
                return false;
            }
        }

        public UserSession BuildUserSession(User user)
        {
            var session = new UserSession
            {
                UserLoginId = user.UserID,
                Username = user.Username,
                Email = user.Email ?? "",
                FirstName = user.UserProfile?.FirstName ?? "",
                LastName = user.UserProfile?.LastName ?? "",
                IsSuperUser = user.UserRoles.Any(ur => ur.Role.RoleName == "SuperAdmin"),
                LoginTime = DateTime.UtcNow
            };

            // Roles
            session.Roles = user.UserRoles.Select(ur => new UserRoleInfo
            {
                RoleID = ur.Role.RoleID,
                RoleName = ur.Role.RoleName,
                Description = ur.Role.Description
            }).ToList();

            // Permissions
            var permissions = user.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission)
                .Distinct()
                .ToList();

            session.Permissions = permissions.Select(p => new UserPermInfo
            {
                PermissionID = p.PermissionID,
                ModuleID = p.ModuleID,
                ModuleName = p.Module?.ModuleName ?? "",
                Action = p.Action,
                Description = p.Description
            }).ToList();

            return session;
        }

        // Optional helper if you need to load permissions outside of login
        public async Task<List<UserPermInfo>> GetUsersPermissionsAsync(int userId)
        {
            return await _userRepo.GetUserPermissionsAsync(userId);
        }

        // Helper method (or move to a utility class)
        private string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }
    }

}

