using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.WinForms.Controls;

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
            ShowControl<LoginControl>(login =>
            {
                login.LoginSucceeded += OnLoginSucceeded;
            });
        }

        public void ShowMainPage()
        {
            ShowControl<LoginControl>(login =>
            {
                login.LoginSucceeded += OnLoginSucceeded;
            });
        }

        private void OnLoginSucceeded(object? sender, EventArgs e)
        {
            ShowMainPage();
        }

        private void OnLogoutRequested(object? sender, EventArgs e)
        {
            ShowLogin();
        }

        public void LoadControl(Control control)
        {
            pnBody.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnBody.Controls.Add(control);
        }

        public void ShowControl<T>(Action<T>? configure = null) where T : Control
        {
            var control = _services.GetRequiredService<T>();
            configure?.Invoke(control);
            LoadControl(control);
        }
    }
}
