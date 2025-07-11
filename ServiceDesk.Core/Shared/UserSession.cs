using ServiceDesk.Core.Models;

namespace ServiceDesk.Core.Shared
{
    public class UserSession
    {
        public int UserLoginId { get; set; }
        public string Username { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
        public string? Email { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<UserRoleInfo> Roles { get; set; } = new();
        public List<UserPermInfo> Permissions { get; set; } = new();

        public DateTime LoginTime { get; set; }
        public bool IsSuperUser { get; set; }

        // Helper methods
        public bool HasPermission(string moduleName, string action)
        {
            if (IsSuperUser) return true;

            return Permissions.Any(p =>
                p.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase) &&
                p.Action.Equals(action, StringComparison.OrdinalIgnoreCase));
        }

        public bool HasAnyPermissionInModule(string moduleName)
        {
            if (IsSuperUser) return true;

            return Permissions.Any(p =>
                p.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase));
        }

        public List<string> GetAllowedModules()
        {
            if (IsSuperUser)
                return new List<string> { "All" };

            return Permissions.Select(p => p.ModuleName).Distinct().ToList();
        }

    }
}
