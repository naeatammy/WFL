using System.ComponentModel.DataAnnotations;

namespace PBL3.Models
{
	public class Role
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<UserRole> ListUser { get; set; }
		
	}
}
