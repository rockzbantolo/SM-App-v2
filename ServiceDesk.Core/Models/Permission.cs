using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDesk.Core.Models
{
    [Table("permissions")]
    public class Permission
    {
        [Key]
        public int PermissionID { get; set; }

        [Required]
        public int ModuleID { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; } // View, Add, Edit, Delete

        [StringLength(255)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        [ForeignKey("ModuleID")]
        public virtual Module Module { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
