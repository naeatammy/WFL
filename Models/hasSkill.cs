using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
    public class hasSkill
    {

        
        [ForeignKey("FreelancerId")]
        public int FreelancerId { get; set; }
        public  Freelancer Freelancer { get; set; }
        [ForeignKey("SkillId")]
        public int SkillId {  get; set; }
        public Skill Skill { get; set; }
    }
}
