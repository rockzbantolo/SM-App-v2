using Microsoft.VisualBasic.ApplicationServices;
using ServiceDesk.Core.Shared;

namespace ServiceDesk.WinForms.Controls
{
    public partial class MainControl : UserControl
    {
        private readonly UserSession _session;

        public MainControl(UserSession session)
        {
            InitializeComponent();
            _session = session;
            SetupAccess();
        }

        private void SetupAccess()
        {
            //btnInventory.Visible = SessionManager.HasAnyPermissionInModule("Inventory");
            //btnServices.Visible = SessionManager.HasAnyPermissionInModule("Services");
        }
    }
}
