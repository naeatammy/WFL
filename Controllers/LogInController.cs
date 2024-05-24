using Microsoft.AspNetCore.Mvc;
using PBL3.Data;

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
    }
}
