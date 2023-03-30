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
            return View();
        }


        public IActionResult Landingplatform()
        {


            CIPlatform.Entitites.ViewModel.Mission missions = _allRepository.missionRepository.GetAllMission();
            return View(missions);


        }
        [HttpPost]

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

        public JsonResult RecommendCoWorker(string[] emailList, long MissionId, string UserId)
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
            long userId = Convert.ToInt64(UserId);
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


    }
}