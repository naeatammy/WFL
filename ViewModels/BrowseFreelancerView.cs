using System.Runtime.CompilerServices;

namespace PBL3.ViewModels
{
    public class BrowseFreelancerView
    {   
        public int id { get; set; }
        public string UserName { set; get; }
        public string Country { set; get; }
        public int Rating { get; set; }
        public List<string> ListSkill { set; get; }
        public List<string> FreelancerSkill { set; get; }
        public string Description { set; get; }

    }
}
