namespace PBL3.ViewModels
{
    public class FreelancerVM
    {
        public string Avatar {  get; set; }
        public int? ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }

        public List<string> FreelancerSkill { set; get; }

    }
}
