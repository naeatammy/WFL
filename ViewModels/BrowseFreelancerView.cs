using PBL3.Models;
using System.Runtime.CompilerServices;

namespace PBL3.ViewModels
{
    public class BrowseFreelancerView
    {   
        public Freelancer freelancer { set; get; }
        
        public List<Skill> ListSkill { set; get; }
        public List<string> FreelancerSkill { set; get; }
       

    }
}
