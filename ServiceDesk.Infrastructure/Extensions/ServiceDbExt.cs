using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Infrastructure.Data;


namespace ServiceDesk.Infrastructure.Extensions
{
    public static class ServiceDbExt
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("Default"),
                    ServerVersion.AutoDetect(config.GetConnectionString("Default"))
                )
            );

            return services;
        }
    }
}
