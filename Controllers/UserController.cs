using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;
using PBL3.ViewModels;
using System.Collections.Specialized;

namespace PBL3.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public UserController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public int? getCurrentUserID() //lay ID cua user dang nhap 
        {
            return HttpContext.Session.GetInt32("UserID");
        }


        public IActionResult Profile()
        {
            int? userID = getCurrentUserID();
            var jobs = dBContext.job.Where(u => u.freelancerID == userID).Where(u => u.JobStatus == "Finished").ToList();
            var freelancer = dBContext.Freelancers.Where(u => u.FreelancerID == userID).Include(u => u.User).Include(u => u.hasskills).ThenInclude(u => u.Skill).FirstOrDefault();
            var client = dBContext.Clients.Where(u => u.ClientID == userID).Include(u => u.User).FirstOrDefault();
            int openProject = dBContext.job.Where(u => u.JobStatus == "Pending").Where(u => u.ClientId == userID).Count();
            int pastProject = dBContext.job.Where(u => u.JobStatus == "Finished").Where(u => u.ClientId == userID).Count();
            int activeProject = dBContext.job.Where(u => u.JobStatus == "In Progress").Where(u => u.ClientId == userID).Count();
            int totalProject = dBContext.job.Where(u => u.ClientId == userID).Count();
            var CTFreviews = dBContext.Review.Include(u => u.Job).Where(u => u.ReviewType == "CTF").Where(u => u.FreelancerID == userID).ToList();
            var FTCreviews = dBContext.Review.Include(u => u.Job).Include(u => u.Freelancer).ThenInclude(u => u.User).Where(u => u.ReviewType == "FTC").Where(u => u.ClientID == userID).ToList();
            var Allskills = dBContext.ListSkill.Select(u => u.Name).ToList();




            List<string> selectedskills = new List<string>();

            foreach (var i in dBContext.hasSkill.Where(u => u.FreelancerId == getCurrentUserID()).ToList())
            {
                var skill = dBContext.ListSkill.FirstOrDefault(u => u.SkillID == i.SkillId);
                selectedskills.Add(skill.Name);
            }
            var item = new ProfileViewModel
            {
                freelancer = freelancer,
                client = client,
                jobcount = dBContext.job.Where(u => u.ClientId == userID).Where(u => u.JobStatus == "Finished").Count(),
                freelancerreviews = CTFreviews,
                Clientreviews = FTCreviews,
                openProject = openProject,
                activeProject = activeProject,
                pastProject = pastProject,
                totalProject = totalProject,
                Allskills = Allskills,
                selectedSkills = selectedskills,


            };

            return View(item);
        }

        [HttpPost]
        public IActionResult EditProfile(ProfileViewModel model)
        {
            int UserID = model.freelancer.FreelancerID;
            var User = dBContext.Users.Where(u => u.ID == model.freelancer.FreelancerID).FirstOrDefault();
            var freelancer = dBContext.Freelancers.Where(u => u.FreelancerID == model.freelancer.FreelancerID).FirstOrDefault();
            var client = dBContext.Clients.Where(u => u.ClientID == model.freelancer.FreelancerID).FirstOrDefault();
            User.FirstName = model.freelancer.User.FirstName;
            User.LastName = model.freelancer.User.LastName;
            freelancer.Description = model.freelancer.Description;
            freelancer.Location = model.freelancer.Location;
            client.Description = model.freelancer.Description;
            client.Location = model.freelancer.Location;
            dBContext.SaveChanges();

            var listskills = dBContext.ListSkill.ToList();
            List<int> selectedskillsID = listskills.Where(u => model.selectedSkills.Contains(u.Name))
                                                   .Select(u => u.SkillID).ToList();

            var oldskills = dBContext.hasSkill.Where(u => u.FreelancerId == UserID).ToList();
            dBContext.hasSkill.RemoveRange(oldskills);
            List<hasSkill> newSkills = selectedskillsID
            .Select(u => new hasSkill
            {
                FreelancerId = UserID,
                SkillId = u
            })
            .ToList();

            dBContext.hasSkill.AddRange(newSkills);
            dBContext.SaveChanges();




            return RedirectToAction("Profile");
        }

        public IActionResult ChangePassword()
        {
            PasswordViewModel item = new PasswordViewModel();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;

            return View(item);
        }
        [HttpPost]

        public IActionResult ChangePassword(PasswordViewModel item)
        {

            string Password = dBContext.Users.Where(u => u.ID == getCurrentUserID()).Select(u => u.Password).FirstOrDefault();

            if (item.currentPW != Password)
            {
                ViewBag.ErrorMessage = "Your Current PassWord is not correct";
                return View();
            }
            else if (item.rePW != item.newPW)
            {
                ViewBag.ErrorMessage = "2 password khong trung nhau";
                return View();
            }



            var user = dBContext.Users.Where(u => u.ID == getCurrentUserID()).FirstOrDefault();
            user.Password = item.newPW;
            dBContext.SaveChanges();

            ViewBag.Success = "Your password doi thanh cong";


            return View();
        }

        [HttpPost]
        public IActionResult AddorUpdateFavour(int id)
        {

            var freelancer = dBContext.Freelancers.Where(u => u.FreelancerID == id).FirstOrDefault();
            var favourite = dBContext.Favourite.Where(u => u.UserID == getCurrentUserID().Value).Where(u => u.freelancerID == id).FirstOrDefault();
            if (favourite != null)
            {
                dBContext.Favourite.Remove(favourite);
                dBContext.SaveChanges();
            }
            else
            {
                Favourite favourite1 = new Favourite
                {
                    UserID = getCurrentUserID(),
                    freelancerID = id,
                    SelectedDate = DateTime.Now,
                };
                dBContext.Favourite.Add(favourite1);
                dBContext.SaveChanges();
            }

            return RedirectToAction("FreelancerDetail", "User", new { id = id });
        }





        public IActionResult BrowseFreelancer()   // hien thi danh sach freelancer
        {
            List<Freelancer> li1 = dBContext.Freelancers.Include(u => u.User)
                .ToList();

            List<BrowseFreelancerView> view = new List<BrowseFreelancerView>();



            foreach (var freelancer in li1)
            {

                List<string> freelancerskills = dBContext.hasSkill.Where(s => s.FreelancerId == freelancer.FreelancerID).Select(s => s.Skill.Name).ToList();



                BrowseFreelancerView item = new BrowseFreelancerView
                {
                    freelancer = freelancer,
                    FreelancerSkill = freelancerskills,
                    ListSkill = dBContext.ListSkill.ToList(),

                };

                view.Add(item);
            }
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserID();


            return View(view);
        }

        //tim kiem freelancer theo 1 so tieu chi
        public IActionResult FilterFreelancer(string searchTerm, string sortOrder, int[] selectedSkills, string sortCountry, int? rating)
        {

            List<Freelancer> li1 = dBContext.Freelancers.Include(u => u.User)
                .ToList();
            List<BrowseFreelancerView> view = new List<BrowseFreelancerView>();
            if (!string.IsNullOrEmpty(sortCountry))
            {
                li1 = li1.Where(u => u.Location.Contains(sortCountry)).ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                li1 = li1.Where(u => u.User.Username.Contains(searchTerm)).ToList();
            }


            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder == "lowestRating")
                {
                    li1 = li1.OrderBy(u => u.Rating).ToList();
                }
                if (sortOrder == "highestRating")
                {
                    li1 = li1.OrderByDescending(u => u.Rating).ToList();
                }
            }
            if (rating.HasValue)
            {
                li1 = li1.Where(f => f.Rating >= rating.Value).ToList();
            }
            if (selectedSkills != null && selectedSkills.Any())
            {
                var s1 = dBContext.hasSkill.Where(s => selectedSkills.Contains(s.SkillId))
                                           .Select(s => s.FreelancerId).ToList();
                li1 = li1.Where(s => s1.Contains(s.FreelancerID)).ToList();
            }


            foreach (var freelancer in li1)
            {

                List<string> freelancerskills = dBContext.hasSkill.Where(s => s.FreelancerId == freelancer.FreelancerID).Select(s => s.Skill.Name).ToList();



                BrowseFreelancerView item = new BrowseFreelancerView
                {
                    freelancer = freelancer,
                    FreelancerSkill = freelancerskills,
                    ListSkill = dBContext.ListSkill.ToList(),

                };

                view.Add(item);
            }
            return PartialView("_FreelancerListPartial", view);
        }

        // chi tiet cua freelancer
        public IActionResult FreelancerDetail(int id)
        {

            AddRecentlyViewedFreelancer(getCurrentUserID().Value, id);
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            var freelancer = dBContext.Freelancers.Include(u => u.User)
                .FirstOrDefault(u => u.FreelancerID == id);
            var user = dBContext.Users.Find(id);
            List<string> freelancerSkills = dBContext.hasSkill
                                                 .Where(u => u.FreelancerId == freelancer.FreelancerID)
                                                 .Select(s => s.Skill.Name)
                                                 .ToList();
            List<Review> freelancerReviews = dBContext.Review.Where(u => u.ReviewType == "CTF")
                                                            .Where(u => u.FreelancerID == id)
                                                            .Include(u => u.Job)
                                                            .ThenInclude(u => u.RequiredSkills)
                                                            .ThenInclude(u => u.Skill)
                                                            .OrderByDescending(u => u.ReviewTime)
                                                            .ToList();
            var favourite = dBContext.Favourite.Where(u => u.UserID == getCurrentUserID().Value).Where(u => u.freelancerID == id).FirstOrDefault();
            bool favour;
            if (favourite != null)
            {
                favour = true;
            }
            else
            {
                favour = false;
            }
            FreelancerDetailViewModel item = new FreelancerDetailViewModel
            {
                freelancer = freelancer,
                Skills = freelancerSkills,
                reviews = freelancerReviews,
                IsFavourite = favour,

            };

            return View(item);
        }

        public void AddRecentlyViewedFreelancer(int userId, int freelancerId)
        {


            var existingRecord = dBContext.RecentlyViews.FirstOrDefault(r => r.UserID == userId && r.FreelancerID == freelancerId);

            if (existingRecord != null)
            {

                existingRecord.DateView = DateTime.Now;
            }
            else
            {
                // Người dùng chưa xem freelancer này trước đó, thêm một bản ghi mới
                var recentlyViewed = new RecentlyView
                {
                    UserID = userId,
                    FreelancerID = freelancerId,
                    DateView = DateTime.Now
                };

                dBContext.RecentlyViews.Add(recentlyViewed);
            }

            dBContext.SaveChanges();

        }

        // hien thi danh sach cong viec
        public IActionResult BrowseJob()
        {


            int? currentUserId = getCurrentUserID();
            ViewBag.CurrentUserId = currentUserId;
            var joblist = dBContext.job.Where(u => u.JobStatus == "Pending").ToList();
            List<BrowseJobView> item = new List<BrowseJobView>();



            foreach (var i in joblist)
            {
                List<string> ListJobSkill = dBContext.RequiredSkill.Where(u => u.JobId == i.ID).Select(s => s.Skill.Name).ToList();
                BrowseJobView job = new BrowseJobView
                {
                    job = i,
                    skills = dBContext.ListSkill.ToList(),
                    SkillName = ListJobSkill,
                };
                item.Add(job);
            }
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;


            return View(item);
        }


        // tim kiem cong viec theo 1 so tieu chi
        [HttpGet]
        public IActionResult FilterJob(string searchTerm, string sortOrder, string[] selectedProjectTypes, int[] selectedSkills, string minPrice, string maxPrice)
        {
            var joblist = dBContext.job.Where(u => u.JobStatus == "Pending").Where(u => u.IsDelete == false).ToList();
            int? currentUserId = getCurrentUserID();
            ViewBag.CurrentUserId = currentUserId;

            List<BrowseJobView> item = new List<BrowseJobView>();


            if (!string.IsNullOrEmpty(searchTerm))
            {
                joblist = joblist.Where(u => u.Name.Contains(searchTerm)).ToList();
            }
            if (!string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = sortOrder.Trim();
                if (sortOrder == "lowestPrice")
                {
                    joblist = joblist.OrderBy(u => u.PaymentAmount).ToList();
                }
                if (sortOrder == "highestPrice")
                {
                    joblist = joblist.OrderByDescending(u => u.PaymentAmount).ToList();
                }
            }

            if (!string.IsNullOrEmpty(minPrice))
            {
                double price = Convert.ToDouble(minPrice);
                joblist = joblist.Where(u => u.PaymentAmount >= price).ToList();
            }
            if (!string.IsNullOrEmpty(maxPrice))
            {
                double price = Convert.ToDouble(maxPrice);
                joblist = joblist.Where(u => u.PaymentAmount <= price).ToList();
            }


            if (selectedProjectTypes != null && selectedProjectTypes.Any())
            {

                joblist = joblist.Where(u => selectedProjectTypes.Contains(u.PaymentType)).ToList();
            }

            if (selectedSkills != null && selectedSkills.Any())
            {
                var jobskills = dBContext.RequiredSkill.Where(u => selectedSkills.Contains(u.SkillId))
                                                       .Select(u => u.JobId).Distinct().ToList();
                joblist = joblist.Where(u => jobskills.Contains(u.ID)).ToList();
            }

            foreach (var i in joblist)
            {
                List<string> ListJobSkill = dBContext.RequiredSkill.Where(u => u.JobId == i.ID).Select(s => s.Skill.Name).ToList();
                BrowseJobView job = new BrowseJobView
                {
                    job = i,
                    skills = dBContext.ListSkill.ToList(),
                    SkillName = ListJobSkill,
                };
                item.Add(job);
            }
            return PartialView("_JobListPartial", item);
        }




        // hien thi chi tiet cong viec
        public IActionResult JobDetail(int id)
        {
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            int? currentUserId = getCurrentUserID();
            ViewBag.CurrentUserId = currentUserId;
            var job = dBContext.job.Include(u => u.Client)
                .ThenInclude(u => u.User)
                .FirstOrDefault(u => u.ID == id);
            var jobRegist = dBContext.jobRegistrations.Where(u => u.JobID == id).ToList();
            List<string> jobSkills = dBContext.RequiredSkill
                                                 .Where(u => u.JobId == job.ID)
                                                 .Select(s => s.Skill.Name)
                                                 .ToList();

            JobDetailView item = new JobDetailView
            {

                job = job,
                ListSkill = jobSkills,
                JobRegistration = jobRegist,


            };




            return View(item);
        }



        // Dang ky cong viec (Khi nguoi dung an apply trong View) ( chua xong)
        [HttpPost]
        public IActionResult ApplyJob(int jobId)
        {
            int? currentUserId = getCurrentUserID();

            JobRegistration registration = new JobRegistration
            {
                FreelancerID = currentUserId.Value,
                JobID = jobId,
                start_time = DateTime.Now,
            };
            dBContext.jobRegistrations.Add(registration);
            dBContext.SaveChanges();

            var listJobregist = dBContext.jobRegistrations.Where(u => u.JobID == jobId).ToList();

            return RedirectToAction("JobDetail", new { id = jobId });
        }


        [HttpPost]
        public IActionResult ApplyFreelancerForJob(int freelancerID, int jobID)
        {


            var i = dBContext.job.Where(u => u.ID == jobID).FirstOrDefault();

            i.freelancerID = freelancerID;
            i.JobStatus = "In Progress";
            dBContext.job.Update(i);
            dBContext.SaveChanges();
            return RedirectToAction("ShowProposals", new { jobId = jobID });
        }






        // danh sach ung tuyen cho 1 cong viec
        public IActionResult ShowProposals(int jobId)
        {
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            var listJobregist = dBContext.jobRegistrations.Where(u => u.JobID == jobId)
                .Include(u => u.freelancer).ThenInclude(u => u.hasskills).ThenInclude(u => u.Skill)
                .Include(u => u.Job).ThenInclude(u => u.Client)
                .Include(u => u.freelancer).ThenInclude(u => u.User)
                .ToList();
            ViewBag.currentUserID = getCurrentUserID();
            ViewBag.JobId = jobId;

            return View(listJobregist);
        }


        //xoa 1 freelancer khoi danh sach ung tuyen cho 1 cong viec
        public IActionResult DeleteProposal(int jobID, int freelancerID)

        {
            var proposal = dBContext.jobRegistrations.Where(u => u.JobID.Equals(jobID) && u.FreelancerID.Equals(freelancerID)).FirstOrDefault();
            dBContext.jobRegistrations.Remove(proposal);
            dBContext.SaveChanges();
            return RedirectToAction("ShowProposals", new { jobId = jobID });
        }




    }
}
