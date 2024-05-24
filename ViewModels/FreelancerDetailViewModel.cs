namespace PBL3.ViewModels
{
    public class FreelancerDetailViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string email { get; set; }
       

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<string> Skills { get; set; }
    }
}
