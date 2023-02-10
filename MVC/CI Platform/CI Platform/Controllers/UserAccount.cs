using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class UserAccount : Controller
    {
        public IActionResult login()
        {
            return View();
        }
        
    }
}
