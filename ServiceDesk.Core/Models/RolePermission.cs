
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ServiceDesk.Core.Models
{
    [Table("rolepermissions")]
    public class RolePermission
    {
        [Key]
        public int RolePermissionID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        public int PermissionID { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public int? AssignedBy { get; set; }

        // Navigation properties
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        [ForeignKey("PermissionID")]
        public virtual Permission Permission { get; set; }
    }
}
