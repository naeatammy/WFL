using PBL3.Models;

namespace PBL3.ViewModels
{
    public class JobDetail
    {
       public User User { get; set; }
        public Client client{ get; set; }
        public Job job { get; set; }
        public List<string> ListSkill { get; set; }
    }
}
