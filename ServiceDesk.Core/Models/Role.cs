using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ServiceDesk.Core.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }

}
