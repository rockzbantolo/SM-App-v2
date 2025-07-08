
namespace ServiceDesk.Core.Shared
{
    public static class EnvMode
    {
        public static bool IsDev(string? env) =>
            env?.Equals("Developer", StringComparison.OrdinalIgnoreCase) == true;

        public static bool IsProd(string? env) =>
            env?.Equals("Production", StringComparison.OrdinalIgnoreCase) == true;
    }
}
