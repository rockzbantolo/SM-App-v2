using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ServiceDesk.Core.Models
{
    [Table("user_auth")] // ✅ Map to your actual table name
    public class UserAuth
    {
        [Key] // primary key
        public int UserLoginId { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }
}
