using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBL3.Models
{
	public class User
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[MaxLength(128)]
		
		public string Username { get; set; }
		public string? Avatar { get; set; }

		[Required]
		[MaxLength(128)]
		public string Password { get; set; }

		[Required]
		[MaxLength(128)]
		
		public string Email { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }
		public DateTime? CreatedAt { get; set; }
		public bool IsBanned { get; set; } = false;
		[ForeignKey("RoleID")]
		public int RoleID { get; set; }
		public Role Role { get; set; }
		
	}
}
