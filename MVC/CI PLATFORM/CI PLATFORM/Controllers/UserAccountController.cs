using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class UserAccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Forgotpassword()
        {
            return View();
        }

        public IActionResult Resetpassword()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
    }
}
