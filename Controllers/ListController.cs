using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;
using PBL3.ViewModels;

namespace PBL3.Controllers
{
    public class ListController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public ListController(ApplicationDBContext context)
        {
            dbContext = context;
        }
        public int? getCurrentUserID()
        {
            return HttpContext.Session.GetInt32("UserID");
            
        }
        private FreelancerVM CreateFreelancerVM(Freelancer freelancer)
        {
            var skills = dbContext.hasSkill
            .Include(u => u.Skill)
            .Where(u => u.FreelancerId == freelancer.FreelancerID)
            .Select(u => u.Skill.Name)
            .ToList();
            return new FreelancerVM
            {
                Avatar = freelancer.User.Avatar,
                ID = freelancer.FreelancerID,
                Name = freelancer.User.FirstName + " " + freelancer.User.LastName,
                UserName = freelancer.User.Username,
                Email = freelancer.User.Email,
                Location = freelancer.Location ?? "No Location",
                Rating = freelancer.Rating ?? 0,
                Description = freelancer.Description ?? "No Description",
                FreelancerSkill = skills,
            };
        }
        public IActionResult Favourite()
        {
            var favouritefreelancerIds = dbContext.Favourite.Where( u => u.UserID == getCurrentUserID())
                                                            .OrderByDescending(u=> u.SelectedDate)
                                                            .Select( u=> u.freelancerID)                                                          
                                                            .ToList();
            var freelancers = dbContext.Freelancers.Include( u => u.User)
                                                   .Where(u => favouritefreelancerIds.Contains(u.FreelancerID))

                                                   .ToList();
            var data = freelancers.Select(u => CreateFreelancerVM(u)).ToList();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserID();
            return View(data);
        }
        public IActionResult MyHires()
        {
           
                var currentUserId = getCurrentUserID();
               

                var hiredFreelancerIds = dbContext.jobRegistrations
                    .Where(jr => jr.Job.ClientId == currentUserId && jr.Job.freelancerID != null)
                    .Select(jr => jr.Job.freelancerID.Value)
                    .Distinct()
                    .ToList(); 

                var freelancers = dbContext.Freelancers
                    .Include(f => f.User)
                    .Where(f => hiredFreelancerIds.Contains(f.FreelancerID))
                    .ToList(); 

                var data = freelancers.Select(f => CreateFreelancerVM(f)).ToList();
                var username = HttpContext.Session.GetString("Username");
                ViewBag.Username = username;
                ViewBag.currentUserID = getCurrentUserID();
                return View(data);
           
        }
        public IActionResult RecentlyViewed()
        { 

            var listrecentlyviewfreelancerIDs = dbContext.RecentlyViews.Where(u => u.UserID == getCurrentUserID().Value)
                                                                      .Select(u => u.FreelancerID)
                                                                      .ToList();
            var freelancer = dbContext.Freelancers.Include(u => u.User).Where(u => listrecentlyviewfreelancerIDs.Contains(u.FreelancerID)).ToList();
            var data= freelancer.Select( u =>  CreateFreelancerVM(u)).ToList();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserID();
            return View(data);
        }

    }
}
