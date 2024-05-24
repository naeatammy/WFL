using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
	public class Review
	{
		[Key]

		public int Id { get; set; }
		[ForeignKey("Job")]
		public int JobID { get; set; }
		public Job Job { get; set; }
		[ForeignKey("Freelancer")]
		public int FreelancerID { get; set; }
		public Freelancer Freelancer { get; set; }
		[ForeignKey("Client")]
		public int ClientID { get; set; } 
		public Client Client { get; set; }
		public DateTime ReviewTime { get; set; }
		public int Grade { get; set; }
		public string Comment { get; set; }
	}
}
