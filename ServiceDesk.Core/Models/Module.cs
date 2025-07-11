using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ServiceDesk.Core.Models
{
    [Table("modules")]
    public class Module
    {
        [Key]
        public int ModuleID { get; set; }

        [Required]
        [StringLength(100)]
        public string ModuleName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Icon { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }

}
