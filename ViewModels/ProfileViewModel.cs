using PBL3.Models;

namespace PBL3.ViewModels
{
    public class ProfileViewModel
    {
        public Freelancer freelancer { get; set; }
        public Client client { get; set; }
        public int openProject { get; set; }
        public int pastProject { get; set; }    
        public int activeProject { get; set; }
        public int totalProject { get; set; }
        public int jobcount { get; set; }
        public List<Review> freelancerreviews { get; set; }
        public List<Review> Clientreviews { get; set; }

        //forEdit
        public List<string> selectedSkills { get; set; }
        public List<string> Allskills { get; set; }

        //forReview
        
       


    }
}
