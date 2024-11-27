using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3.Data;
using PBL3.Models;

namespace PBL3.Controllers
{
	public class SignUpController : Controller
	{	private readonly ApplicationDBContext dBContext;
		public SignUpController(ApplicationDBContext dbContext)
		{
			this.dBContext = dbContext;
		}
		public IActionResult SignUp()
        {
           
            return View();
		}
		[HttpPost]
		public IActionResult SignUp(User user)
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
                user.CreatedAt = DateTime.Now;
                user.RoleID = 1;
                dBContext.Users.Add(user);

                dBContext.SaveChanges();
                Freelancer freelancer = new Freelancer { FreelancerID = user.ID };
                dBContext.Freelancers.Add(freelancer);
                Client client = new Client { ClientID = user.ID };
                dBContext.Clients.Add(client);
                dBContext.SaveChanges();
                              
            }
            return View();
        }
        public bool IsUsernameUnique(string username, int id)
        {
            return !dBContext.Users.Any(u => u.Username == username && u.ID != id);

        }
        private bool IsEmailUnique(string email, int id)
        {
            return !dBContext.Users.Any(u => u.Email == email && u.ID != id);
        }
    }
}
