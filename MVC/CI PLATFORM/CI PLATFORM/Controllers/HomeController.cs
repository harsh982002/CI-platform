using CI_PLATFORM.Models;
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
            List<CIPlatform.Entitites.ViewModel.Mission> missions = _allRepository.missionRepository.GetAllMission();
            return View(missions);
        }
        [HttpPost]
        
        public JsonResult Landingplatform(List<string> countries, List<string> cities, List<string> themes, List<string> skills, string key, string sort_by, int page_index)
        {
            if (page_index != 0)
            {
                List<CIPlatform.Entitites.ViewModel.Mission> missions = _allRepository.missionRepository.GetFilteredMissions(countries, cities, themes, skills, sort_by, page_index);
                
                return Json(new { mission = missions, length = missions.Count });
            }
           
            if (key is not null)
            {
                List<CIPlatform.Entitites.ViewModel.Mission> search_missions = _allRepository.missionRepository.GetSearchMissions(key, page_index);
                return Json(new { missions = search_missions, success = true, lenght = search_missions.Count });
            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.Mission> missions = _allRepository.missionRepository.GetFilteredMissions(countries, cities, themes, skills, sort_by, page_index);
                return Json(new { missions, success = true, lenght = missions.Count });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Volunteermission(int Id)
        {

            VolunteerViewModel mission_detail = _allRepository.volunteerRepository.Missiondetails(Id);

            return View(mission_detail);
        }
    
            public ActionResult AddToFavourite(long MissionId,string UserId)
        {
            long userId = Convert.ToInt64(UserId);

            bool mission = _allRepository.AddFavouriteMission(MissionId, userId);
            if (mission == true)
            {

                TempData["Success"] = "Added to your faviroute";
            }
            else
            {

                TempData["Success"] = "Removed from your faviroute";
            }
            return View();
        }


        [HttpPost]
       
        public IActionResult Storylisting()
        {
            return View();
        }
    }
}