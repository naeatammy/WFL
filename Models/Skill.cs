using System.ComponentModel.DataAnnotations;

namespace PBL3.Models
{
	public class Skill
	{
		[Key]	
		public int SkillID { get; set; }
		public string Name { get; set; }
		public List<hasSkill> freelancerskills { get; set; }
		public List<RequiredSkill> jobskills { get; set; }
	}
}
