using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;

using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CI_PLATFORM.Controllers
{
    public class UserAccountController : Controller
    {

        public readonly IAccountRepository _AccountRepo;
        public UserAccountController(IAccountRepository userRepository)
        {
            _AccountRepo = userRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User obj)
        {
            var user = _AccountRepo.Login(obj);
            if (user == null)
            {
                TempData["success"] = "Invalid Username or Password";
                return View();
            }
            TempData["success"] = "Login Successfully...";
            return RedirectToAction("Landingplatform", "UserAccount");
        }

        [HttpGet]
        public IActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(User obj)
        {
            var user = _AccountRepo.Forgot(obj);
            if (user == null)
            {
                TempData["success"] = "Invalid Email";
                return View();
            }
            TempData["success"] = "Check your email to reset password";
            return RedirectToAction("Resetpassword");
        }

        [HttpGet]
        public IActionResult Resetpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Resetpassword(User obj, string token)
        {
            var validToken = _AccountRepo.Reset(obj, token);

            if (validToken != null)
            {
                TempData["success"] = "Your Password is changed";
                return RedirectToAction("Login");
            }
            TempData["success"] = "Token not Found";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User obj)
        {
            var user = _AccountRepo.Register(obj);
            if (user != null)
            {
                TempData["success"] = "User already exists.";
                return RedirectToAction("Login", "UserAccount");
            }
            //if (ModelState.IsValid)
            //{
            //_UserRepository.Users.Add(obj);
            //_db.SaveChanges();
            TempData["success"] = "Registration Successfull";
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
