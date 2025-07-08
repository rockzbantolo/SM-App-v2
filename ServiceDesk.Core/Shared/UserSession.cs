using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDesk.Core.Shared
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Optional: store allowed permissions/forms
        public List<UserPermission> Permissions { get; set; } = new();

        public bool IsLoggedIn => UserId > 0;

        public void Clear()
        {
            UserId = 0;
            Username = string.Empty;
            RoleId = 0;
            RoleName = string.Empty;
            Permissions.Clear();
        }
    }
}
