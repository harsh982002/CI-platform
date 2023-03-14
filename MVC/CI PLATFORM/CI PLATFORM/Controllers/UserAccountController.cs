
using CIPlatform.Entities;
using CI_PLATFORM.Helpers;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Entitites.Models;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CIPlatform.Entitites.ViewModel;

namespace CI_PLATFORM.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IAccountRepository _registerInterface;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;


        public UserAccountController(ILogger<HomeController> logger, IAccountRepository accountInterface, IHttpContextAccessor httpContextAccessor,
                              IConfiguration _configuration)
        {
            _registerInterface = accountInterface;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _registerInterface.LoginViewModel(model);
                    if (user == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found or invalid password ");
                    }
                    else
                    {
                        HttpContext.Session.SetString("Email", user.Email);
                        HttpContext.Session.SetString("Name", user.FirstName + " " + user.LastName);
                        HttpContext.Session.SetString("Avtar", user.Avatar);
                        return RedirectToAction("Landingplatform", "Home");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }


        [HttpGet]
        public IActionResult registration()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Registration(RegistrationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (_registerInterface.IsValidUserEmail(model))
                    {
                        User registertion = _registerInterface.RegistrationViewModel(model);
                        return RedirectToAction("Login", "UserAccount");

                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "This Mail Account Already Register !! Please Check your mail or Login your Account...");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }

        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {

            return View();
        }


        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {


                var user = _registerInterface.ForgotPasswordViewModel(model);
                if (user == null)
                {
                    TempData["success"] = "Invalid Email";
                    return View();
                }


                TempData["success"] = "Check your email to reset password";
                return RedirectToAction("Resetpassword");
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult Resetpassword()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Resetpassword(ResetPasswordViewModel model, string token)
        {
            if (ModelState.IsValid)
            {
                var validToken = _registerInterface.ResetPasswordViewModel(model, token);

                if (validToken != null)
                {
                    TempData["success"] = "Your Password is changed";
                    return RedirectToAction("Login");
                }
                TempData["success"] = "Token not Found";
                return RedirectToAction("Index");
            }
            return View(model);
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
