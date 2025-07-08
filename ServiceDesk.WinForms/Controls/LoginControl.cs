using ServiceDesk.Core.Models;
using ServiceDesk.Core.Interfaces;
using ServiceDesk.Core.Shared;
using System.Data;

namespace ServiceDesk.WinForms.Controls
{
    public partial class LoginControl : UserControl
    {
        public event EventHandler? LoginSucceeded;

        private readonly IUserRepository _userRepo;
        private readonly UserSession _session;

        public LoginControl(IUserRepository userRepo, UserSession session)
        {
            InitializeComponent();
            _userRepo = userRepo;
            _session = session;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = "admin";
            string password = "admin";

            var user = await _userRepo.GetByCredentialsAsync(username, password);

            if (user != null)
            {
                Logger.Info($"Login succeeded for User: {user.Username}");

                _session.UserId = user.UserLoginId;
                //_session.RoleId = user.RoleId;
                //_session.RoleName = user.Role?.Name ?? "Unknown";

                LoginSucceeded?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Invalid User");
            }

        }

        private async void LoadUsers()
        {
            var users = await Task.Run(() =>
            {
                var allUsers = _userRepo.GetAllUsers();

                // Use IQueryable for DB-side filtering
                var filtered = allUsers.Where(u => u.Username.StartsWith("a"));

                // Use IEnumerable (client-side logic)
                IEnumerable<UserAuth> materialized = filtered.ToList();

                return materialized.Select(u => new
                {
                    u.UserLoginId,
                    u.Username
                }).ToList(); // Now it's IEnumerable (in-memory)
            });

            // Bind to a DataGridView
            //dataGridView1.DataSource = users;
        }

    }
}
