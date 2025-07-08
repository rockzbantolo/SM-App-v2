using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDesk.Infrastructure.Extensions
{
    public static class ServiceSessionExt
    {
        public static IServiceCollection AddSessionServices(this IServiceCollection services)
        {
            services.AddSingleton<UserSession>();
            return services;
        }
    }
}
