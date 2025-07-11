using Microsoft.EntityFrameworkCore;
using ServiceDesk.Core.Interfaces;
using ServiceDesk.Core.Models;
using ServiceDesk.Infrastructure.Data;


namespace ServiceDesk.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            try
            {
                user.PasswordHash = password; // or use BCrypt if supported
                user.CreatedAt = DateTime.UtcNow;

                var success = await _userRepo.AddAsync(user);
                if (!success)
                    throw new Exception("Failed to add user to the database.");

                // Log Activity
                await LogAsync(user.UserID, "Add User", "Add New User", "User Management");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create user: {ex.Message}", ex);
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                user.UpdatedAt = DateTime.UtcNow;

                var success = await _userRepo.UpdateAsync(user);
                if (!success)
                    throw new Exception("Failed to update user.");

                // Log Activity
                await LogAsync(user.UserID, "Update User", "Update User Details", "User Management");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update user: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                return await _userRepo.DeleteAsync(userId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllUsersAsync();
        }


        public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
        {
            try
            {
                return await _userRepo.AssignRoleAsync(userId, roleId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveRoleFromUserAsync(int userId, int roleId)
        {
            try
            {
                return await _userRepo.RemoveRoleAsync(userId, roleId);
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<Role>> GetUserRolesAsync(int userId)
        {
            return await _userRepo.GetUserRolesAsync(userId);
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _userRepo.GetAllUsersAsync();
        }

        public async Task LogAsync(int userId, string type, string desc, string module)
        {
            await _userRepo.LogActivityAsync(new UserActivity
            {
                UserID = userId,
                ActivityType = type,
                ActivityDescription = desc,
                ModuleName = module
            });
        }
   
    }
}
