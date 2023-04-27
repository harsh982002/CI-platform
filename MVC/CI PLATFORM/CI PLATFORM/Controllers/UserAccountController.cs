
using CIPlatform.Entities;
using CI_PLATFORM.Helpers;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Entitites.Models;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace CI_PLATFORM.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IAccountRepository _registerInterface;
        private readonly IAllRepository _allRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;


        public UserAccountController(ILogger<HomeController> logger,IAllRepository allRepository, IAccountRepository accountInterface, IHttpContextAccessor httpContextAccessor,
                              IConfiguration _configuration)
        {
            _registerInterface = accountInterface;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
            _allRepository = allRepository;
        }

        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "UserAccount");
        }

       
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (HttpContext.Session.GetString("UserId") is not null)
            {
                if (returnUrl is not null)
                {
                    return new RedirectResult(returnUrl);
                }
                return RedirectToAction("Landingplatform", "Home");
            }
            else
            {
                if(returnUrl is not null)
                {
                    TempData["returnUrl"] = returnUrl;
                }
            }
            ViewBag.bannerlist = _allRepository.cmsRepository.GetBanner().Bans;
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            ViewBag.bannerlist = _allRepository.cmsRepository.GetBanner().Bans;
            try
            {
                if (ModelState.IsValid)
                {
                    User user = _registerInterface.LoginViewModel(model);
                    
                    if (user == null)
                    {
                        ViewBag.Message = String.Format("User Does Not Exist, Please Register Yourself.");
                        return View();
                       /* return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User doesn't exist please Register yourself.");*/
                    }
                   
                    else if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password) == false)
                    {
                      
                        ViewBag.Message = String.Format("Password is incorrect");
                        return View();
                    }
                    else
                    {
                        if (user.CountryId is not null)
                        {
                            bool verify = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                            if (verify)
                            {
                                HttpContext.Session.SetString("Email", user.Email);
                                var userId = _registerInterface.GetUserID(user.Email);
                                HttpContext.Session.SetString("UserId", userId.ToString());
                                HttpContext.Session.SetString("Country", user.CountryId.ToString());
                                HttpContext.Session.SetString("City", user.CityId.ToString());
                                if(user.Avatar is not null)
                                {
                                    HttpContext.Session.SetString("Avtar", user.Avatar);
                                }
                                HttpContext.Session.SetString("Name", user.FirstName + " " + user.LastName);

                                if(user.Role == "Admin")
                                {
                                    if (TempData.ContainsKey("returnUrl"))
                                    {
                                        var url = TempData["returnUrl"] as string;
                                        return new RedirectResult(url);
                                    }
                                    HttpContext.Session.SetString("role",user.Role);
                                    return RedirectToAction("CMS", "Admin");
                                }
                                
                                    if (TempData.ContainsKey("returnUrl"))
                                    {
                                        var url = TempData["returnUrl"] as string;
                                        return new RedirectResult(url);
                                    }
                                    return RedirectToAction("Landingplatform", "Home");
                                
                               
                            }
                        }
                        else
                        {
                            HttpContext.Session.SetString("Email", user.Email);
                            var userId = _registerInterface.GetUserID(user.Email);
                            HttpContext.Session.SetString("UserId", userId.ToString());
                            HttpContext.Session.SetString("Name", user.FirstName + " " + user.LastName);
                            return RedirectToAction("ProfilePage", "Home");
                        }

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
                        ViewBag.Message = String.Format("This Mail Account Already Register !! Please Check your mail or Login your Account...");
                        return View();
                     
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



    }
}
