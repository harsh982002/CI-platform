using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class UserAccountController : Controller
    {

        private readonly CiplatformContext _db;

        public UserAccountController(CiplatformContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(string Email ,string Password)
        {
            if(Email != null || Password != null)
            {
                var findUser = _db.Users.FirstOrDefault(x => x.Email == Email || x.Password == Password);
                if (findUser != null)
                {
                    return RedirectToAction("Landingplatform");
                }
            }
            return RedirectToAction("Registration");
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
        [HttpPost]
        public IActionResult Registration(User user)
        {
            var findUser = _db.Users.FirstOrDefault(x => x.Email == user.Email);
            if (findUser == null)

            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }

            return RedirectToAction("Registration");
        }

        public IActionResult Landingplatform()
        {
            return View();
        }

        public IActionResult Volunteermission()
        {
            return View();
        }

        public IActionResult Storylisting()
        {
            return View();
        }

    }
}
