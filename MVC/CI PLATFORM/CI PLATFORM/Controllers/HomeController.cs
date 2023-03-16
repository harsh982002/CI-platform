using CI_PLATFORM.Models;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
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
            if (key is not null)
            {
                List<CIPlatform.Entitites.ViewModel.Mission> search_missions = _allRepository.missionRepository.GetSearchMissions(key, page_index);
                return Json(new { missions = search_missions, success = true });
            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.Mission> missions = _allRepository.missionRepository.GetFilteredMissions(countries, cities, themes, skills, sort_by, page_index);
                return Json(new { missions, success = true });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}