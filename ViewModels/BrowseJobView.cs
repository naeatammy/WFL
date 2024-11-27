using PBL3.Models;

namespace PBL3.ViewModels
{
    public class BrowseJobView
    {
        public Job job { set; get; }
        public List<Skill> skills { set; get; }   
        public List<string> SkillName { set; get; }

    }
}
