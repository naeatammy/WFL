using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
	public class Freelancer
	{
		[Key, ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int FreelancerID { get; set; }
		public virtual User User { get; set; } 
		
		public string? Location { get; set; }
		public int? Rating { get; set; }
		public string? Description { get; set; }
		
		public List<hasSkill>	hasskills { get; set; }
		public List<Review> freelancerreviews { get; set; }
		public List<JobRegistration> jobregistrations { get; set; }


	}
}
