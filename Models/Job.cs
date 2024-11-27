using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBL3.Models
{


	public class Job
	{
		[Key]
		public int ID { get; set; } // Khóa chính

		[ForeignKey("Client")]
		public int ClientId { get; set; } // Khóa ngoại tham chiếu đến ClientId trong bảng Client
		public Client Client { get; set; } // Đối tượng đại diện cho khóa ngoại
		public Nullable<int> freelancerID { get; set; }	

		public string Name { get; set; }
		public string Description { get; set; }
		public string ExpectedDuration { get; set; }
		public string PaymentType { get; set; }
		public double PaymentAmount { get; set; }
		public DateTime CreateTime { get; set; }
		public string JobStatus { get; set; }
		public string CreateBy { get; set; }
		public bool IsDelete { get; set; } = false;

		public List<Review> Reviews { get; set; }
		public List<RequiredSkill> RequiredSkills { get; set; }
		public List<JobRegistration> listjobregist { get; set; }
	}
}

