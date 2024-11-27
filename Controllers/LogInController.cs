using Microsoft.AspNetCore.Mvc;
using PBL3.Data;
using PBL3.Models;

namespace PBL3.Controllers
{
    public class LogInController : Controller
    {   private readonly ApplicationDBContext dbContext;
        public LogInController(ApplicationDBContext dBContext)
        {
            this.dbContext = dBContext;
            
        }
        public IActionResult LogIn()
        {

            return View();
           
        }
        [HttpPost]
        public IActionResult LogIn(User user)
        {
            var u = dbContext.Users.Where( i => i.Username.Equals(user.Username) && i.Password.Equals(user.Password) ).FirstOrDefault();
            if(u != null)
            {
                HttpContext.Session.SetString("Username", u.Username);
                HttpContext.Session.SetInt32("UserID", u.ID);
                if(u.IsBanned == true)
                {
                    ViewBag.LoginFail = "Your account has been banned!!";
                    return View();
                }
                if(u.RoleID == 1 )
                {
                    return RedirectToAction("Favourite", "List");
                }
                else
                {
                    return RedirectToAction("admincrud", "Admin");                }
               
            }
            else
            {
                ViewBag.LoginFail="Failed, Check ur Username or Password";
                return View();
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
