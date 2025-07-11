using ServiceDesk.Core.Shared;
using ServiceDesk.Core.Services;

namespace ServiceDesk.WinForms.Controls
{
    public partial class LoginControl : UserControl
    {
        public event EventHandler? LoginSucceeded;

        private readonly IAuthService _auth;

        public LoginControl(IAuthService auth)
        {
            InitializeComponent();
            _auth = auth;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = "Admin";
            string password = "admin";

            /*
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            */

            var session = await _auth.LoginAsync(username, password);

            if (session != null)
            {
                // Raise login success event
                LoginSucceeded?.Invoke(this, EventArgs.Empty);
                Logger.Info($"Login succeeded for user: {session.Username} roleID: {session.UserLoginId}");
            }
            else
            {
                MessageBox.Show("Invalid login.");
            }
        }
    }
}
