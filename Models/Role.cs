using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
	public class Role
	{
		[Key]
		public int RoleID { get; set; }
		public string Name { get; set; }
		
		
	}
}
