using PBL3.Models;

namespace PBL3.ViewModels
{
    public class JobDetailView
    {
       
        public Job job { get; set; }
        public List<JobRegistration> JobRegistration { get; set; }
        public List<string> ListSkill { get; set; }
    }
}
