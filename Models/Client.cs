using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
	public class Client
	{
		[Key, ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ClientID { get; set; }
		public virtual User User { get; set; }
		
		public string? Location { get; set; }
		public int? Rating { get; set; }
		public string? Description { get; set; }
		
		public List<Job> jobs { get; set; }
		public List<Review> clientreviews { get; set; }
	}
}
