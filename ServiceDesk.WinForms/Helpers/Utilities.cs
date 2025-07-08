
namespace ServiceDesk.WinForms.Helpers
{
    internal class Utilities
    {
        public static void LoadControl(UserControl control, Panel panel)
        {
            panel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel.Controls.Add(control);
        }

    }
}
