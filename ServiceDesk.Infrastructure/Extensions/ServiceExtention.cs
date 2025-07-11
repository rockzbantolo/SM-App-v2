using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Core.Shared;
using ServiceDesk.Infrastructure.Data;

namespace ServiceDesk.Infrastructure.Extensions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Database
            services.AddDatabase(config);

            // Settings & Configuration
            services.AddSettings(config);

            // Repositories
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDataContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("Default"),
                    ServerVersion.AutoDetect(config.GetConnectionString("Default"))
                )
            );
            return services;
        }

        private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
        {
            // Register Env Settings
            var envSettings = new EnvSetting();
            config.GetSection("EnvSetting").Bind(envSettings);
            services.AddSingleton(envSettings);

            // Register to Logger
            Logger.Init(envSettings);

            // Add Infrastructure Services
            services.AddSingleton<UserSession>();

            return services;
        }
    }
}
