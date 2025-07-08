using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Core.Shared;


namespace ServiceDesk.Infrastructure.Extensions
{
    public static class ServiceEnvExt
    {
        public static IServiceCollection AddEnvironmentSettings(this IServiceCollection services, IConfiguration config)
        {
            var envSettings = new EnvSetting();
            config.GetSection("EnvSetting").Bind(envSettings);

            Logger.Init(envSettings);
            services.AddSingleton(envSettings);

            return services;
        }
    }
}
