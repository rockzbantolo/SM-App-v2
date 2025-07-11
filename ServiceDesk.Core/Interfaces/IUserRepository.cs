using ServiceDesk.Core.Models;

namespace ServiceDesk.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByUserIdAsync(int userId);
        Task<bool> UpdatePasswordAsync(int userId, string newPasswordHash);

        Task<List<User>> GetAllUsersAsync();
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int userId);
        Task<bool> SetActiveStatusAsync(int userId, bool isActive);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);

        Task<bool> AssignRoleAsync(int userId, int roleId);
        Task<bool> RemoveRoleAsync(int userId, int roleId);
        Task<List<Role>> GetUserRolesAsync(int userId);
        Task<List<UserPermInfo>> GetUserPermissionsAsync(int userId);
        Task LogActivityAsync(UserActivity activity);

    }
}
