
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
            
            return View();

        }

    }
}
