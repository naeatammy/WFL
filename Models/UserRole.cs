using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
    public class UserRole
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
