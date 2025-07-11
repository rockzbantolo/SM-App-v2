

namespace ServiceDesk.Core.Models
{
    public class UserPermInfo
    {
        public int PermissionID { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
