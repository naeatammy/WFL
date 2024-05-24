using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;
using PBL3.ViewModels;
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
            return View();
		}
		public IActionResult ShowUsers()
		{	
			var User = dBContext.Users.ToList();
            return View(User);
        }
		public IActionResult UserDetail(int id)
		{	
			var user = dBContext.Users.FirstOrDefault(x => x.ID == id);
			return View(user);
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
                return RedirectToAction("ShowUsers");
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
				dBContext.Users.Remove(obj);
				dBContext.SaveChanges();
                
            }
            return RedirectToAction("ShowUsers");
        }
        public IActionResult ShowJob()
        {
			var job = dBContext.job.ToList();
			
            return View(job);
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
			{	Id = job.ID,
				ClientID = job.ClientId,
				Name = job.Name,
				Description= job.Description,
				ExpectedDuration = job.ExpectedDuration,
				CreateBy = job.CreateBy,
				CreateTime = job.CreateTime,
				JobStatus = job.JobStatus,
				PaymentAmount = job.PaymentAmount,
				PaymentType = job.PaymentType,
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
			List<Skill> selectedskills = dBContext.ListSkill.ToList();
            //List<string> listSkillNames = dBContext.ListSkill.Select(x => x.Name).ToList();


            //jobview.ListSkillNames = listSkillNames;
            Job job = new Job {
				ClientId = jobview.ClientID,
				Name = jobview.Name,
				Description = jobview.Description,
				ExpectedDuration = jobview.ExpectedDuration,
				CreateBy = jobview.CreateBy,
				CreateTime = DateTime.Now,
			JobStatus = jobview.JobStatus,
			PaymentAmount = jobview.PaymentAmount,
			PaymentType = jobview.PaymentType,
			

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
            //var requiredSkillIds = dBContext.RequiredSkill
            //.Where(u => u.JobId == id).ToList();
			 var requiredSkillIds = dBContext.ListSkill.ToList();
            List<string> listSkill = new List<string>();

            foreach (var skill in requiredSkillIds)
            {
                
				
                listSkill.Add(skill.Name);
            }
            var jobview = new JobViewModel
            {
                Id = job.ID,
                ClientID = job.ClientId,
                Name = job.Name,
                Description = job.Description,
                ExpectedDuration = job.ExpectedDuration,
                JobStatus = job.JobStatus,
                PaymentAmount = job.PaymentAmount,
                PaymentType = job.PaymentType,
                SelectedSkillName = listSkill,

            };
            return View(jobview);
		}
		[HttpPost]
		public IActionResult JobEdit(JobViewModel jobview)
		{
            List<Skill> selectedskills = dBContext.ListSkill.ToList();
			 
			Job job = new Job
            {	ID= jobview.Id,
                ClientId = jobview.ClientID,
                Name = jobview.Name,
                Description = jobview.Description,
                ExpectedDuration = jobview.ExpectedDuration,
                CreateBy = jobview.CreateBy,
                CreateTime = DateTime.Now,
                JobStatus = jobview.JobStatus,
                PaymentAmount = jobview.PaymentAmount,
                PaymentType = jobview.PaymentType,


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
    }
}
