using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDesk.Core.Models
{
    [Table("user_activity")]
    public class UserActivity
    {
        [Key]
        public int ActivityID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string ActivityType { get; set; }

        public string? ActivityDescription { get; set; }

        public DateTime ActivityTimestamp { get; set; } = DateTime.UtcNow;

        [StringLength(45)]
        public string? IPAddress { get; set; }

        [StringLength(255)]
        public string? UserAgent { get; set; }

        [StringLength(100)]
        public string? ModuleName { get; set; }

        public string? AdditionalData { get; set; }

        // Optional Navigation
        [ForeignKey("UserID")]
        public virtual User? User { get; set; }
    }
}
