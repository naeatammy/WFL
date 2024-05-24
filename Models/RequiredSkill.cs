using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
    public class RequiredSkill
    {
        [ForeignKey("JobId")]
        public int JobId {  get; set; }
        public Job Job { get; set; }
        [ForeignKey("SkillId")]
        public int SkillId {  get; set; }
        public Skill Skill { get; set; }
    }
}
