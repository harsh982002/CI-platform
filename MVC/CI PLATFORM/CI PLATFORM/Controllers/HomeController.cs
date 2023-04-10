using CI_platform.Areas.User.Controllers;
using CI_PLATFORM.Models;
using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CI_PLATFORM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllRepository _allRepository;
       


        public HomeController(IAllRepository allRepository)
        {
            _allRepository = allRepository;
          

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            if(HttpContext.Session.GetString("Country") is not null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ProfilePage","Home");
            }
           
        }

        [Route("/Home/Landingplatform")]
        public IActionResult Landingplatform()
        {
            if(HttpContext.Session.GetString("Country") is not null) {

                CIPlatform.Entitites.ViewModel.Mission missions = _allRepository.missionRepository.GetAllMission();
                return View(missions);
            }
            else
            {
                return RedirectToAction("ProfilePage", "Home");
            }


        }
        [HttpPost]
        [Route("/Home/Landingplatform")]
        public JsonResult Landingplatform(List<string> countries, List<string> cities, List<string> themes, List<string> skills, string key, string sort_by, int page_index, long user_id, long mission_id)
        {
            if (key is not null)
            {
                CIPlatform.Entitites.ViewModel.Mission search_missions = _allRepository.missionRepository.GetSearchMissions(key, page_index);
                var filtered_missions = this.RenderViewAsync("mission_partial", search_missions, true);
                return Json(new { mission = filtered_missions, success = true, length = search_missions.Missions.Count });
            }



            else if (page_index != 0)
            {
                CIPlatform.Entitites.ViewModel.Mission missions = _allRepository.missionRepository.GetFilteredMissions(countries, cities, themes, skills, sort_by, page_index, user_id);
                var page_missions = this.RenderViewAsync("mission_partial", missions, true);
                return Json(new { mission = page_missions, length = missions.Missions.Count });
            }
            else
            {
                CIPlatform.Entitites.ViewModel.Mission missions = _allRepository.missionRepository.GetFilteredMissions(countries, cities, themes, skills, sort_by, page_index, user_id);
                var Cities = this.RenderViewAsync("City_partial", missions, true);
                var filtered_missions = this.RenderViewAsync("mission_partial", missions, true);
                return Json(new { mission = filtered_missions, city = Cities, success = true, length = missions.Missions.Count });
            }
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Home/Volunteermission/{id}")]
        public IActionResult Volunteermission(long id, long UserId)
        {
          
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "UserAccount");
            }
            
            
                long userId = long.Parse(HttpContext.Session.GetString("UserId"));
                CIPlatform.Entitites.ViewModel.VolunteerViewModel mission = _allRepository.missionRepository.Mission(id, userId);


                return View(mission);
           
           


        }

        public JsonResult AddToFavourite(long MissionId, string UserId)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));

            bool mission = _allRepository.missionRepository.AddFavouriteMission(MissionId, userId);
            if (mission == true)
            {

                return Json(mission);
            }
            else
            {

                return Json(null);
            }

        }

        public JsonResult RecommendCoWorker(string[] emailList, long MissionId)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));

            if (emailList != null)
            {
                var mail = _allRepository.missionRepository.sendMail(emailList, MissionId, userId);
                return Json(mail);
            }
            return Json(null);
        }

     

        public JsonResult ApplyMission(long MissionId, long UserId)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            bool application = _allRepository.missionRepository.ApplyMission(MissionId, userId);
            if (application == true)
            {
                return Json(application);
            }
            else
            {
                return Json(null);
            }
        }
        public void AddComment(string Comment, long MissionId, string UserId)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            _allRepository.missionRepository.AddComment(Comment, MissionId, userId);
        }

        [HttpPost]
        [Route("/Home/Volunteermission/{mission_id}")]
        public JsonResult RateMission(long mission_id, int rating)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            var Rating = _allRepository.missionRepository.addRatings(rating, mission_id, userId);
            return Json(Rating);
        }

        [Route("/Home/Profile")]
        public IActionResult ProfilePage()
        {
            var userId = long.Parse(HttpContext.Session.GetString("UserId"));

            CIPlatform.Entitites.ViewModel.ProfileViewModel details = _allRepository.profileRepository.Get_details(0, userId);
            if(details?.CountryId is not null)
            {
                HttpContext.Session.SetString("Country", details?.CountryId.ToString());
            }
            if (details?.Avatar is not null)
            {
                HttpContext.Session.SetString("Avtar", details?.Avatar);
            }
            if(details?.FirstName is not null && details?.LastName is not null)
            {
                HttpContext.Session.SetString("Name", details?.FirstName + " " + details?.LastName);
            }
            return View(details);
        }

        [HttpPost]
        [Route("/Home/Profile")]
        public IActionResult ProfilePage(CIPlatform.Entitites.ViewModel.ProfileViewModel model, int country, string? oldpassword, string? newpassword)
        {
            
           
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            if (oldpassword is not null && newpassword is not null)
            {
                bool success = _allRepository.profileRepository.Change_Password(oldpassword, newpassword, userId);
                return Json(new { success });
            }
            if (ModelState.IsValid)
            {
                if(country != 0)
                {
                   
                    CIPlatform.Entitites.ViewModel.ProfileViewModel details = _allRepository.profileRepository.Get_details(country, userId);
                    var cities = this.RenderViewAsync("Profilecity_partial", details, true);
                    return Json(new {cities = cities});
                }

                else
                {
                    bool success = _allRepository.profileRepository.profile_update(model, userId);
                    return RedirectToAction("ProfilePage");
                }

            }
            else
            {
                if (country != 0)
                {
                    CIPlatform.Entitites.ViewModel.ProfileViewModel details = _allRepository.profileRepository.Get_details(country, userId);
                    var cities = this.RenderViewAsync("ProfileCity_partial", details, true);
                    return Json(new { cities = cities });
                }
                else
                {
                    bool success = _allRepository.profileRepository.profile_update(model, userId);
                    return RedirectToAction("ProfilePage");
                }
            }
        }
        [HttpPost]
        [Route("/Home/contact")]
        public IActionResult Contact(string subject , string message)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            var uname = HttpContext.Session.GetString("Name");
            var uemail = HttpContext.Session.GetString("Email");
            bool success = _allRepository.profileRepository.contact_us(userId, uname, uemail, subject, message);
            return Json(new {success});
        }
        public IActionResult Volunteertimesheet()
        {
            return View();
        }

    }
}