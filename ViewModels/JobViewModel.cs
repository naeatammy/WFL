using Microsoft.AspNetCore.Mvc.Rendering;
using PBL3.Models;

namespace PBL3.ViewModels
{
    public class JobViewModel
    {
            
        public Job job { get; set; }
        public List<string> ListSkillNames { get; set; }
        
        public List<string> SelectedSkillName { get; set; }
    }
}
