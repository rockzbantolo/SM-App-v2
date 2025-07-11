using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ServiceDesk.Core.Models
{
    [Table("userroles")]
    public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int RoleID { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public int? AssignedBy { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}
