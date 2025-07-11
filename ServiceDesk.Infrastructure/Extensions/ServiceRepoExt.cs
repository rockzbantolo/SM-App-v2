using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Core.Interfaces;
using ServiceDesk.Core.Services;
using ServiceDesk.Infrastructure.Repositories;

namespace ServiceDesk.Infrastructure.Extensions
{
    public static class ServiceRepoExt
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();

            // Add more repositories here if needed to UI...

            return services;
        }
    }
}
