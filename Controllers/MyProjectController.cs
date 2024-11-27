using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;
using PBL3.ViewModels;
using System.Configuration;
using System.Numerics;

namespace PBL3.Controllers
{
    public class MyProjectController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public MyProjectController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public int? getCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserID");
        }
        public IActionResult UpdateBrowse(string text = "")
        {
            if (text == "")
            {
                return PartialView("ResponseBrowseNull");
            }
            var list = new CombineList
            {
                listFreelancer = dBContext.Freelancers.Include(p => p.User).Where(p => p.User.Username.Contains(text)).OrderBy(p => p.Rating).Take(6).ToList(),
                listProject = dBContext.job.Where(p => p.Name.Contains(text)).Where(p => p.JobStatus == "Pending").Where(p => p.IsDelete == false).OrderByDescending(p => p.PaymentAmount).Take(2).ToList(),
            };
            return PartialView("ResponseBrowse", list);
        }
        public IActionResult ClientMyProjectOpenProject()
        {
            var project = dBContext.job.Include(p => p.RequiredSkills).Where(p => p.IsDelete == false)
                .Where(p => p.ClientId == getCurrentUserId())
                .Where(p => p.JobStatus != "In progress")
                .Where(p => p.JobStatus != "Finished")
                .OrderBy(p => p.ClientId)
                .ToList();
            var projectToShow = project.Take(10).ToList();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserId();
            return View(projectToShow);
        }
        [HttpPost]
        public IActionResult UpdateTableOP(int pageSize, string text = "", string sort = "ClientID")
        {
            switch (sort)
            {
                case "ClientID":
                    var project = dBContext.job.Include(p => p.RequiredSkills).Where(p => p.IsDelete == false)
                       .Where(p => p.ClientId == getCurrentUserId())
                   .Where(p => p.JobStatus != "In progress")
                   .Where(p => p.JobStatus != "Finished")
                   .Where(p => p.Description.Contains(text))
                   .OrderBy(p => p.ClientId)
                   .ToList();
                    var projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "Description":
                    project = dBContext.job.Include(p => p.RequiredSkills).Where(p => p.IsDelete == false)
                        .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus != "In progress")
                    .Where(p => p.JobStatus != "Finished")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.Description)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "PaymentAmount":
                    project = dBContext.job.Include(p => p.RequiredSkills).Where(p => p.IsDelete == false)
                    .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus != "In progress")
                    .Where(p => p.JobStatus != "Finished")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.PaymentAmount)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                default: break;
            }
            return PartialView("ResponseTable");
        }
        public IActionResult ClientMyProjectWorkPast()
        {
            var project = dBContext.job.Where(p => p.IsDelete == false)
                .Where(p => p.ClientId == getCurrentUserId())
                .Where(p => p.JobStatus == "In progress")
                .ToList();
            var projectToShow = project.Take(10).ToList();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserId();
            return View(projectToShow);
        }
        [HttpPost]
        public IActionResult UpdateTableWP(int pageSize, string text = "", string sort = "ClientID")
        {
            switch (sort)
            {
                case "ClientID":
                    var project = dBContext.job.Include(u => u.Client).ThenInclude(u => u.User).Where(p => p.IsDelete == false)
                   .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus == "In progress")
                   .Where(p => p.Description.Contains(text))
                   .OrderBy(p => p.ClientId)
                   .ToList();
                    var projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "Description":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus == "In progress")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.Description)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "PaymentAmount":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus == "In progress")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.PaymentAmount)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                default: break;
            }
            return PartialView("ResponseTable");
        }

        public IActionResult ClientMyProjectPastProject()
        {
            var project = dBContext.job.Where(p => p.IsDelete == false).Where(p => p.ClientId == getCurrentUserId())
                .Where(p => p.JobStatus == "Finished")
                .ToList();
            var projectToShow = project.Take(10).ToList();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserId();
            return View(projectToShow);
        }
        [HttpPost]
        public IActionResult UpdateTablePP(int pageSize, string text = "", string sort = "ClientID")
        {
            switch (sort)
            {
                case "ClientID":
                    var project = dBContext.job.Include(u => u.Client).ThenInclude(u => u.User).Where(p => p.IsDelete == false)
                   .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus == "Finished")
                   .Where(p => p.Description.Contains(text))
                   .OrderBy(p => p.ClientId)
                   .ToList();
                    var projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "Description":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus == "Finished")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.Description)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "PaymentAmount":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.ClientId == getCurrentUserId())
                    .Where(p => p.JobStatus == "Finished")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.PaymentAmount)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                default: break;
            }
            return PartialView("ResponseTable");
        }
        [HttpPost]
        public IActionResult DeleteJob(string jobID)
        {
            var list = dBContext.job.Where(u => u.ID == Convert.ToInt32(jobID.Trim())).ToList();
            foreach (var item in list)
            {
                dBContext.job.Where(u => u.ID == item.ID).FirstOrDefault().IsDelete = true;
            }
            dBContext.SaveChanges();
            return RedirectToAction();
        }

        [HttpPost]
        public IActionResult SubmitJob(string jobID)
        {
            var list = dBContext.job.Where(u => u.ID == Convert.ToInt32(jobID.Trim())).ToList();
            foreach (var item in list)
            {
                dBContext.job.Where(u => u.ID == item.ID).FirstOrDefault().JobStatus = "Finished";
            }
            dBContext.SaveChanges();
            return RedirectToAction();
        }

        [HttpPost]
        public IActionResult EditJob(string jobID, string name, string des, string duration, string paymentType, string paymentAmount)
        {
            var job = dBContext.job.Find(Convert.ToInt32(jobID.Trim()));
            if (job != null)
            {
                job.Name = name.Trim();
                job.Description = des.Trim();
                job.ExpectedDuration = duration.Trim();
                job.PaymentAmount = Convert.ToDouble(paymentAmount.Trim());
                job.PaymentType = paymentType.Trim();
            }
            dBContext.job.Update(job);
            dBContext.SaveChanges();
            return RedirectToAction();
        }
        [HttpPost]
        public IActionResult ReviewFreelancer(string jobID, string rating, string review)
        {
            var job = dBContext.job.Find(Convert.ToInt32(jobID.Trim()));
            if (job != null)
            {
                dBContext.Review.Add(new Review
                {
                    JobID = job.ID,
                    FreelancerID = Convert.ToInt32(job.freelancerID),
                    ClientID = job.ClientId,
                    ReviewTime = DateTime.Now,
                    Grade = Convert.ToInt32(rating.Trim()),
                    Comment = review.Trim(),
                    ReviewType = "CTF"
                });
                dBContext.SaveChanges();
            }

            var freelancer = dBContext.Freelancers.Find(job.freelancerID);
            if (freelancer != null)
            {
                if (freelancer.Rating == null)
                {
                    freelancer.Rating = (float)Convert.ToDouble(rating.Trim());
                }
                else
                {
                    double bf = Convert.ToDouble(freelancer.Rating);
                    double at = Convert.ToDouble(rating.Trim());
                    freelancer.Rating = (float)((bf + at) / 2);
                }
            }
            dBContext.Freelancers.Update(freelancer);
            dBContext.SaveChanges();
            return RedirectToAction();
        }
        public IActionResult FreelancerMyProjectCurrentWork()
        {
            var project = dBContext.job.Where(p => p.IsDelete == false)
                .Where(p => p.freelancerID == getCurrentUserId())
                .Where(p => p.JobStatus == "In progress")
                .ToList();
            var projectToShow = project.Take(10).ToList();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserId();
            return View(projectToShow);
        }
        public IActionResult FreelancerMyProjectPastWork()
        {
            var project = dBContext.job.Where(p => p.IsDelete == false).Where(p => p.freelancerID == getCurrentUserId())
                .Where(p => p.JobStatus == "Finished")
                .ToList();
            var projectToShow = project.Take(10).ToList();
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            ViewBag.currentUserID = getCurrentUserId(); 
            return View(projectToShow);
        }
        [HttpPost]
        public IActionResult UpdateTableCW(int pageSize, string text = "", string sort = "ClientID")
        {
            switch (sort)
            {
                case "ClientID":
                    var project = dBContext.job.Include(u => u.Client).ThenInclude(u => u.User).Where(p => p.IsDelete == false)
                   .Where(p => p.freelancerID == getCurrentUserId())
                    .Where(p => p.JobStatus == "In progress")
                   .Where(p => p.Description.Contains(text))
                   .OrderBy(p => p.ClientId)
                   .ToList();
                    var projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "Description":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.freelancerID == getCurrentUserId())
                    .Where(p => p.JobStatus == "In progress")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.Description)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "PaymentAmount":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.freelancerID == getCurrentUserId())
                    .Where(p => p.JobStatus == "In progress")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.PaymentAmount)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                default: break;
            }
            return PartialView("ResponseTable");
        }

        [HttpPost]
        public IActionResult UpdateTablePW(int pageSize, string text = "", string sort = "ClientID")
        {
            switch (sort)
            {
                case "ClientID":
                    var project = dBContext.job.Include(u => u.Client).ThenInclude(u => u.User).Where(p => p.IsDelete == false)
                   .Where(p => p.freelancerID == getCurrentUserId())
                    .Where(p => p.JobStatus == "Finished")
                   .Where(p => p.Description.Contains(text))
                   .OrderBy(p => p.ClientId)
                   .ToList();
                    var projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "Description":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.freelancerID == getCurrentUserId())
                    .Where(p => p.JobStatus == "Finished")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.Description)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                case "PaymentAmount":
                    project = dBContext.job.Where(p => p.IsDelete == false)
                    .Where(p => p.freelancerID == getCurrentUserId())
                    .Where(p => p.JobStatus == "Finished")
                    .Where(p => p.Description.Contains(text))
                    .OrderBy(p => p.PaymentAmount)
                    .ToList();
                    projectToShow = project.Take(pageSize).ToList();
                    return PartialView("ResponseTable", projectToShow);
                default: break;
            }
            return PartialView("ResponseTable");
        }
        [HttpPost]
        public IActionResult ReviewClient(string jobID, string rating, string review)
        {
            var job = dBContext.job.Find(Convert.ToInt32(jobID.Trim()));
            if (job != null)
            {
                dBContext.Review.Add(new Review
                {
                    JobID = job.ID,
                    FreelancerID = Convert.ToInt32(job.freelancerID),
                    ClientID = job.ClientId,
                    ReviewTime = DateTime.Now,
                    Grade = Convert.ToInt32(rating.Trim()),
                    Comment = review.Trim(),
                    ReviewType = "FTC"
                });
                dBContext.SaveChanges();
            }

            var client = dBContext.Clients.Find(job.ClientId);
            if (client != null)
            {
                if (client.Rating == null)
                {
                    client.Rating = (float)Convert.ToDouble(rating.Trim());
                }
                else
                {
                    double bf = Convert.ToDouble(client.Rating);
                    double at = Convert.ToDouble(rating.Trim());
                    client.Rating = (float)((bf + at) / 2);
                }
            }
            dBContext.Clients.Update(client);
            dBContext.SaveChanges();
            return RedirectToAction();
        }
    }
}
