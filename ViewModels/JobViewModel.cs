using Microsoft.AspNetCore.Mvc.Rendering;

namespace PBL3.ViewModels
{
    public class JobViewModel
    {
            
        public int Id { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpectedDuration { get; set; }
        public string PaymentType { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime CreateTime { get; set; }
        public string JobStatus { get; set; }
        public string CreateBy { get; set; }
        public List<string> ListSkillNames { get; set; }
        
        public List<string> SelectedSkillName { get; set; }
    }
}
