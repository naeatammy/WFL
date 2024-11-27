using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;
using PBL3.ViewModels;
using System.Linq;
namespace PBL3.Controllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDBContext dBContext;
		public AdminController(ApplicationDBContext dBContext)
		{
			this.dBContext = dBContext;
		}


		
		public IActionResult admincrud()
		{
            ViewData["Layout"] = null;
            var User = dBContext.Users.ToList();
            return View(User);
		}
		public IActionResult ShowUsers()
		{	
			var User = dBContext.Users.ToList();
            return View(User);
        }

		public IActionResult FilterUser(string searchTerm, string seletedStatus)
		{
			var User = dBContext.Users.ToList();
			if(!string.IsNullOrEmpty(searchTerm))
			{
                User = dBContext.Users.Where(u => u.Username.Contains(searchTerm)).ToList();
            }
			if(!string.IsNullOrEmpty(seletedStatus))
			{
				if(seletedStatus == "Disabled")
				{
                    User = dBContext.Users.Where(u => u.IsBanned == true).ToList();
                }
				if(seletedStatus == "Active")
				{
                    User = dBContext.Users.Where(u => u.IsBanned == false).ToList();
                }
			}
            
			
			return PartialView("_PartialUserList", User);
		}




		public IActionResult UserDetail(int id)
		{	
			var user = dBContext.Users.FirstOrDefault(x => x.ID == id);
			return View(user);
		}
		[HttpPost]
		public IActionResult UserDetail()
		{
			return RedirectToAction("admincrud");
		}
		public IActionResult Create()
		{		
			return View();
		}
		[HttpPost]
		public IActionResult Create(User user)
		{
			
			if(!IsUsernameUnique(user.Username,user.ID))
			{
				ModelState.AddModelError("Username", "Trung UserName");
			}
			if (!IsEmailUnique(user.Email, user.ID))
			{
				ModelState.AddModelError("Email", "Trung Email");
			}
			if (IsEmailUnique(user.Email, user.ID) && IsUsernameUnique(user.Username, user.ID))
			{	
				user.CreatedAt= DateTime.Now;
				user.Avatar = "";
				user.RoleID = 1;
				dBContext.Users.Add(user);
				
				dBContext.SaveChanges();
				Freelancer freelancer = new Freelancer { FreelancerID = user.ID };
				dBContext.Freelancers.Add(freelancer);
				Client client = new Client { ClientID = user.ID };
				dBContext.Clients.Add(client);
				dBContext.SaveChanges();
				return RedirectToAction("ShowUsers");
			}
			return View();	

		}
		public bool IsUsernameUnique(string username, int id)
		{
			return !dBContext.Users.Any(u => u.Username == username && u.ID!=id);

		}
		private bool IsEmailUnique(string email, int id)
		{
			return !dBContext.Users.Any(u => u.Email == email && u.ID != id);
		}

        public IActionResult Edit(int id)
        {
			var user = dBContext.Users.Find(id);
            return View(user);
        }
		[HttpPost]
		public IActionResult Edit(User user)
		{
			user.Avatar = "";
            user.RoleID = 1;
            if (!IsUsernameUnique(user.Username, user.ID))
            {
                ModelState.AddModelError("Username", "Trung UserName");
            }
            if (!IsEmailUnique(user.Email, user.ID))
            {
                ModelState.AddModelError("Email", "Trung Email");
            }
            if (IsEmailUnique(user.Email, user.ID) && IsUsernameUnique(user.Username, user.ID))
            {
                dBContext.Users.Update(user);
                dBContext.SaveChanges();
                return RedirectToAction("admincrud");
            }


            return View();
			
		}
		public IActionResult Delete(int id)
		{
			var user = dBContext.Users.Find(id);
			return View(user);

		}
		[HttpPost]
        public IActionResult Delete (User user)
		{
			var obj = dBContext.Users.Find(user.ID);
				if(obj != null)
			{
				var jobregist = dBContext.jobRegistrations.Where(u => u.FreelancerID == user.ID).ToList();
				var reviewlist = dBContext.Review.Where( u => u.FreelancerID == user.ID || u.ClientID == user.ID).ToList();
				var joblist = dBContext.job.Where(u => u.freelancerID == user.ID || u.ClientId == user.ID).ToList();
				foreach(var model in joblist)
				{
					model.IsDelete = true;
				}
				
				
				
				dBContext.jobRegistrations.RemoveRange(jobregist);
                dBContext.SaveChanges();
                dBContext.Review.RemoveRange(reviewlist);
                dBContext.SaveChanges();
                dBContext.Users.Remove(obj);
				dBContext.SaveChanges();
                
            }
            return RedirectToAction("admincrud");
        }




















        public IActionResult ShowJob()
        {
            var job = dBContext.job.Where(u => u.IsDelete == false).ToList();


            return View(job);
        }
		public IActionResult FilterJob(string searchTerm, string seletedStatus, string sortOrder)
		{
            var job = dBContext.job.Where(u => u.IsDelete == false).ToList();
            if (!string.IsNullOrEmpty(searchTerm))
			{
                job = dBContext.job.Where(u => u.Name.Contains(searchTerm)).ToList();

            }
            if (!string.IsNullOrEmpty(seletedStatus))
            {
                if(seletedStatus == "Fixed Hour")
				{
                    job = dBContext.job.Where(u => u.PaymentType.Contains(seletedStatus)).ToList();
                }
                if (seletedStatus == "Fixed Price")
                {
                    job = dBContext.job.Where(u => u.PaymentType.Contains(seletedStatus)).ToList();
                }

            }

			if(!string.IsNullOrEmpty(sortOrder))
			{
                if (sortOrder == "minPrice")
                {
                    job = dBContext.job.OrderBy(u => u.PaymentAmount).Where(u => u.IsDelete == false).ToList();
                }
                if (sortOrder == "maxPrice")
                {
                    job = dBContext.job.OrderByDescending(u => u.PaymentAmount).Where(u => u.IsDelete == false).ToList();
                }
            }
            return PartialView("_PartialJobList",job);
		}

		public IActionResult JobDetail(int id)
		{
			var job= dBContext.job.FirstOrDefault(x => x.ID == id);
			var requiredSkillIds = dBContext.RequiredSkill
			.Where(u => u.JobId == id).ToList();
 
            List<string> listSkill = new List<string>();
			foreach(var skill in requiredSkillIds)
			{
				var listskills = dBContext.ListSkill.FirstOrDefault(u => u.SkillID == skill.SkillId);
				listSkill.Add(listskills.Name);
			}
			var jobview = new JobViewModel
			{	job = job,
				ListSkillNames = listSkill,
				

			};
			return View(jobview);
		}


		public IActionResult JobAdd()
		{



            JobViewModel jobview = new JobViewModel();
       

            List<string> listSkillNames = dBContext.ListSkill.Select(x => x.Name).ToList();
			
            
            jobview.ListSkillNames = listSkillNames;
            

            return View(jobview);

        }
		[HttpPost]
		public IActionResult JobAdd(JobViewModel jobview)
		{

			var currentuserID = HttpContext.Session.GetInt32("UserID");
            List<Skill> selectedskills = dBContext.ListSkill.ToList();
            
            Job job = new Job {
				ClientId = jobview.job.ClientId,
				Name = jobview.job.Name,
				Description = jobview.job.Description,
				ExpectedDuration = jobview.job.ExpectedDuration,
                CreateBy = dBContext.Clients.Include(u => u.User).Where(u => u.ClientID == jobview.job.ClientId).Select(u => u.User.Username).FirstOrDefault(),
                CreateTime = DateTime.Now,

			JobStatus = jobview.job.JobStatus,
			PaymentAmount = jobview.job.PaymentAmount,
			PaymentType = jobview.job.PaymentType,
			

			};
            dBContext.job.Add(job);
			dBContext.SaveChanges();
            List<Skill> skills = dBContext.ListSkill.ToList();

            List<int> selectedSkillIds = skills
            .Where(s => jobview.SelectedSkillName.Contains(s.Name))
            .Select(s => s.SkillID)
            .ToList();


			List<RequiredSkill> requiredSkills = selectedSkillIds
             .Select(skillId => new RequiredSkill
             {
                 JobId = job.ID,  
                 SkillId = skillId
             })
             .ToList();

            dBContext.RequiredSkill.AddRange(requiredSkills);
            dBContext.SaveChanges();

			return RedirectToAction("ShowJob");
		}

      


        public IActionResult JobEdit(int id)
		{
			var job = dBContext.job.Find(id);
            
			 var requiredSkillIds = dBContext.RequiredSkill.Where( u => u.JobId == id).ToList();
            List<string> listSkill = new List<string>();

            foreach (var skill in requiredSkillIds)
            {
                var skills = dBContext.ListSkill.FirstOrDefault(u => u.SkillID == skill.SkillId);

                listSkill.Add(skills.Name);
            }
			var jobview = new JobViewModel
			{
				job = job,
				SelectedSkillName = listSkill,
				ListSkillNames = dBContext.ListSkill.Select( u => u.Name).ToList(),
				

            };
            return View(jobview);
		}
		[HttpPost]
		public IActionResult JobEdit(JobViewModel jobview)
		{
            List<Skill> selectedskills = dBContext.ListSkill.ToList();
			 
			Job job = new Job
            {	ID= jobview.job.ID,
                ClientId = jobview.job.ClientId,
                Name = jobview.job.Name,
                Description = jobview.job.Description,
                ExpectedDuration = jobview.job.ExpectedDuration,
                CreateBy = dBContext.Clients.Include(u => u.User).Where( u => u.ClientID == jobview.job.ClientId).Select(u => u.User.Username).FirstOrDefault(),
                CreateTime = DateTime.Now,
                JobStatus = jobview.job.JobStatus,
                PaymentAmount = jobview.job.PaymentAmount,
                PaymentType = jobview.job.PaymentType,


            };
            dBContext.job.Update(job);
            dBContext.SaveChanges();
            List<Skill> skills = dBContext.ListSkill.ToList();

            List<int> selectedSkillIds = skills
            .Where(s => jobview.SelectedSkillName.Contains(s.Name))
            .Select(s => s.SkillID)
            .ToList();


            List<RequiredSkill> requiredSkillsToRemove = dBContext.RequiredSkill.Where(rs => rs.JobId == job.ID).ToList();
            dBContext.RequiredSkill.RemoveRange(requiredSkillsToRemove);
            dBContext.SaveChanges();

            List<RequiredSkill> requiredSkillsToAdd = selectedSkillIds
			.Select(skillId => new RequiredSkill
			{
            JobId = job.ID,
            SkillId = skillId
			})
			.ToList();

            dBContext.RequiredSkill.AddRange(requiredSkillsToAdd);
            dBContext.SaveChanges();
            return RedirectToAction("ShowJob");
        }
        public void JobDelete(int jobID)
        {
			var job = dBContext.job.Find(jobID);
			job.IsDelete = true;
			dBContext.job.Update(job);
			dBContext.SaveChanges();
           
			
        }
    }
    
}
		