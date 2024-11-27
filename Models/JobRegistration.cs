using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
	public class JobRegistration
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Freelancer")]
		public int FreelancerID { get; set; }
		public Freelancer freelancer { get; set; }
		[ForeignKey("Job")]
		public int JobID { get; set; }

		public Job Job { get; set; }
		public DateTime start_time { get; set; }
		public DateTime? end_time { get; set; }



	}
}
