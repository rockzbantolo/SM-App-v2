using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.WinForms.Controls;
using static ServiceDesk.WinForms.Helpers.Utilities;

namespace ServiceDesk.WinForms
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _services;

        public MainForm(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
            ShowLogin();
        }

        public void ShowLogin()
        {
            var login = _services.GetRequiredService<LoginControl>();

            login.LoginSucceeded += OnLoginSucceeded; // listen to login event
            LoadControl(login, pnBody);
        }

        private void OnLoginSucceeded(object? sender, EventArgs e)
        {
            ShowMainLayout();
        }

        public void ShowMainLayout()
        {
            //var layout = _services.GetRequiredService<MainControl>();
            //layout.LogoutRequested += OnLogoutRequested; // handle logout
            //LoadControl(layout, pnBody);
        }

        private void OnLogoutRequested(object? sender, EventArgs e)
        {
            ShowLogin();
        }

    }
}
