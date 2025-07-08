using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Core.Interfaces;
using ServiceDesk.Infrastructure.Repositories;

namespace ServiceDesk.Infrastructure.Extensions
{
    public static class ServiceRepoExt
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            // Add more repositories here...

            return services;
        }
    }
}
