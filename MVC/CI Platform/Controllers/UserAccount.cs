using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class UserAccount : Controller
    {
        public IActionResult login()
        {
            return View();
        }

        public IActionResult forgotpassword()
        {
            return View();
        }

        public IActionResult resetpassword()
        {
            return View();
        }

        public IActionResult register()
        {
            return View();
        }

    }
}
