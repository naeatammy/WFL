using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;
using PBL3.ViewModels;

namespace PBL3.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public PostController(ApplicationDBContext context)
        {
            this.dbContext = context;
        }
        public int? getCurrentUserID()
        {
            return HttpContext.Session.GetInt32("UserID");
        }
        public string? getCurrentUserName()
        {
            return HttpContext.Session.GetString("Username");
        }
        public IActionResult PostAProject()
        {
            var data = new List<Skill>();
            foreach (var i in dbContext.ListSkill)
            {
                data.Add(i);
            }
            return View(data);
        }
        /*[HttpPost]
        public IActionResult Update(string name)
        {
            var job = new Job
            {
                Name = name
            };
            dbContext.job.Add(job);
            dbContext.SaveChanges();
            return View();
        }*/
        [HttpPost]
        public ActionResult Create([FromBody] JobViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                var job = new Job
                {
                    ClientId = (int)getCurrentUserID(),
                    Name = model.job.Name,
                    Description = model.job.Description,
                    ExpectedDuration = model.job.ExpectedDuration,
                    PaymentType = model.job.PaymentType,
                    PaymentAmount = model.job.PaymentAmount,
                    CreateTime = DateTime.Now,
                    JobStatus = "Pending",
                    
                    CreateBy = (string)getCurrentUserName(),
                    freelancerID = null,
                    IsDelete = false,
                };

                dbContext.job.Add(job);
                dbContext.SaveChanges();

                var requiredSkills = model.SelectedSkillName.Select(skillId => new RequiredSkill
                {
                    JobId = job.ID,
                    SkillId = int.Parse(skillId)
                }).ToList();

                dbContext.RequiredSkill.AddRange(requiredSkills);
                dbContext.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public ActionResult Success()
        {
            return View(); // Return a success view
        }
        
    }
}
