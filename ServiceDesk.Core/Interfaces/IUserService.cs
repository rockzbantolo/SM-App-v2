using ServiceDesk.Core.Models;


namespace ServiceDesk.Core.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user, string password);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<List<User>> GetAllUsersAsync();

        Task<bool> AssignRoleToUserAsync(int userId, int roleId);
        Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
        Task<List<Role>> GetUserRolesAsync(int userId);
        Task LogAsync(int userId, string type, string desc, string module);
    }
}
