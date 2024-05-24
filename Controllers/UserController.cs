using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;
using PBL3.ViewModels;

namespace PBL3.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public UserController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
       
        public IActionResult BrowseFreelancer()
        {
            List<Freelancer> li1 = dBContext.Freelancers.ToList();
            List<User> li2 = dBContext.Users.ToList();
            List<BrowseFreelancerView> view = new List<BrowseFreelancerView>();
            List<string> listskills = dBContext.ListSkill.Select(u => u.Name).ToList();
            
            
            foreach (var freelancer in li1)
            {

                List<string> freelancerskills = dBContext.hasSkill.Where(s => s.FreelancerId == freelancer.FreelancerID).Select(s => s.Skill.Name).ToList();
             
                var user = li2.FirstOrDefault(u => u.ID == freelancer.FreelancerID);

                BrowseFreelancerView item = new BrowseFreelancerView
                {
                    id= user.ID,
                    UserName = user.Username,
                    Description = freelancer.Description,
                    Rating = freelancer.Rating ?? 0,
                    FreelancerSkill = freelancerskills,
                    ListSkill = listskills,
                    Country = freelancer.Location
                };

                view.Add(item);
            }
            
            return View(view);
        }
        
        public IActionResult FreelancerDetail(int id)
        {
            var freelancer = dBContext.Freelancers.FirstOrDefault(u => u.FreelancerID == id);
            var user = dBContext.Users.Find(id);
            List<string> freelancerSkills = dBContext.hasSkill
                                                 .Where(u => u.FreelancerId == freelancer.FreelancerID)
                                                 .Select(s => s.Skill.Name)
                                                 .ToList();
            FreelancerDetailViewModel item = new FreelancerDetailViewModel {
                UserName = user.Username,
                Id = id,
                Rating = freelancer.Rating ??0,
                Description = freelancer.Description,
                Skills = freelancerSkills,
                Country = freelancer.Location,
                email = user.Email,
                CreatedAt = user.CreatedAt,
                FirstName= user.FirstName,
                LastName = user.LastName

            };




            return View(item);
        }

        public IActionResult BrowseJob()
        {
            var joblist = dBContext.job.ToList();
            List<BrowseJobView> item = new List<BrowseJobView>();

            foreach(var i in joblist)
            {
                List<string> ListJobSkill = dBContext.RequiredSkill.Where(u => u.JobId == i.ID).Select(s => s.Skill.Name).ToList();
                BrowseJobView job = new BrowseJobView
                {
                    job = i,
                  
                    SkillName = ListJobSkill,
                };
                item.Add(job);
            }
            
            

            return View(item);
        }
        public IActionResult JobDetail(int id)
        {
            var job = dBContext.job.FirstOrDefault(u => u.ID == id);
            var user = dBContext.Users.Find(id);
            List<string> jobSkills = dBContext.RequiredSkill
                                                 .Where(u => u.JobId == job.ID)
                                                 .Select(s => s.Skill.Name)
                                                 .ToList();
            




            return View();
        }
    }
}
