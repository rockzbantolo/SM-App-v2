using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.Infrastructure.Extensions;
using ServiceDesk.WinForms.Extentions;


namespace ServiceDesk.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            var config = LoadConfig();

            // Register services
            services
            .AddInfrastructure(config)
            .AddControls();

            // Build the service provider
            using var provider = services.BuildServiceProvider();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(provider.GetRequiredService<MainForm>());
        }

        private static IConfiguration LoadConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}