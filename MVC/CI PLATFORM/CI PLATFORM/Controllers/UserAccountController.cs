using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewDataModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                User user = _registerInterface.LoginViewModel(model);
                if (user == null)
                {
                    return RedirectToAction("Registration");
                }
                else
                {
                    return RedirectToAction("Landingplatform");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }


        public IActionResult Forgotpassword()
        {
            return View();
        }

        public IActionResult Resetpassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationViewModel model)
        {
            try
            {
                User registertion = _registerInterface.RegistrationViewModel(model);
                if (registertion == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Bad request");

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
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
