using PBL3.Models;

namespace PBL3.ViewModels
{
    public class FreelancerDetailViewModel
    {
        public Freelancer freelancer { set; get; }
        public DateTime? CreatedAt { get; set; }
        public List<string> Skills { get; set; }
        public List<Review> reviews { get; set; }
        public Boolean IsFavourite { get; set; }


    }
}
