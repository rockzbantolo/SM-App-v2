
namespace ServiceDesk.Core.Shared
{
    public class SessionManager
    {
        private static UserSession? _currentUserSession;

        public static UserSession? CurrentUser
        {
            get => _currentUserSession;
            private set => _currentUserSession = value;
        }

        public static void SetCurrentUser(UserSession userSession)
        {
            CurrentUser = userSession;
        }

        public static void ClearCurrentUser()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;

        public static bool HasPermission(string moduleName, string action)
        {
            return CurrentUser?.HasPermission(moduleName, action) ?? false;
        }

        public static bool HasAnyPermissionInModule(string moduleName)
        {
            return CurrentUser?.HasAnyPermissionInModule(moduleName) ?? false;
        }

        public static bool IsSuperUser => CurrentUser?.IsSuperUser ?? false;

        public static List<string> GetAllowedModules()
        {
            return CurrentUser?.GetAllowedModules() ?? new List<string>();
        }
    }
}
