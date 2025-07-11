using ServiceDesk.Core.Models;
using ServiceDesk.Core.Shared;

namespace ServiceDesk.Core.Services
{
    public interface IAuthService
    {
        Task<UserSession?> LoginAsync(string username, string password);
        Task<bool> LogoutAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        // Optional: if you want to manually fetch session info elsewhere
        UserSession BuildUserSession(User user);
        Task<List<UserPermInfo>> GetUsersPermissionsAsync(int userId);
    }
}
