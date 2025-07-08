using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.WinForms.Controls;

namespace ServiceDesk.WinForms.Extentions
{
    public static class ServiceControlExt
    {
        public static IServiceCollection AddControls(this IServiceCollection services)
        {
            services.AddTransient<LoginControl>();
            //services.AddTransient<MainControl>();
            //services.AddTransient<HomeControl>();
            //services.AddTransient<SettingsControl>();

            return services;
        }
    }
}
