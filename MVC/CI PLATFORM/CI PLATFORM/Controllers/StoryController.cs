
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PLATFORM.Controllers
{
    public class StoryController : Controller
    {
        private readonly IAllRepository _allRepository;


        public StoryController(IAllRepository allRepository)
        {
                _allRepository = allRepository;
        }

        public IActionResult Story()
        {
            long user_id = long.Parse(HttpContext.Session.GetString("UserId"));
            CIPlatform.Entitites.ViewModel.Mission stories = _allRepository.storyRepository.GetStories(user_id);
            return View(stories);
        }

        public IActionResult ShareStory()
        {
            long user_id = long.Parse(HttpContext.Session.GetString("UserId"));
            List<CIPlatform.Entitites.Models.Mission> missions = _allRepository.storyRepository.mission_of_user(user_id);
            return View(missions);

        }

        [HttpPost]
       
        public JsonResult ShareStory(long story_id, long mission_id, string title, string mystory, List<string> media, string type)
        {
            long user_id = long.Parse(HttpContext.Session.GetString("UserId"));
            if (type == "PUBLISHED")
            {
                bool success = _allRepository.storyRepository.AddStory(user_id, story_id, mission_id, title, mystory, media, type);
                return Json(new { success });
            }
            else
            {
                bool success = _allRepository.storyRepository.AddStory(user_id, 0, mission_id, title, mystory, media, type);
                return Json(new { success });
            }

        }

       

    }
}
