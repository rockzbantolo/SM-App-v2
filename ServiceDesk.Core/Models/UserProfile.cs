using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDesk.Core.Models
{
    [Table("userprofiles")]
    public class UserProfile
    {
        [Key]
        public int UserProfileID { get; set; }

        [Required]
        public int UserID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }

}
