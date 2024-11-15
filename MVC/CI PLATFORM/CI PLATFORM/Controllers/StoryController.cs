﻿
using AspNetCoreHero.ToastNotification.Abstractions;
using CI_platform.Areas.User.Controllers;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PLATFORM.Controllers
{
    public class StoryController : Controller
    {
        private readonly IAllRepository _allRepository;
        private readonly INotyfService _notyf;

        public StoryController(IAllRepository allRepository, INotyfService notyf)
        {
            _allRepository = allRepository;
            _notyf = notyf;
        }

        public IActionResult Story()
        {
            
           
            if(HttpContext.Session.GetString("UserId") is not null)
            {
                long user_id = long.Parse(HttpContext.Session.GetString("UserId"));
                if (HttpContext.Session.GetString("Country") is not null)
                {
                    CIPlatform.Entitites.ViewModel.Mission stories = _allRepository.storyRepository.GetStories(user_id);
                    return View(stories);
                }
                else
                {
                    return RedirectToAction("ProfilePage", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }
           
               
        }

        [HttpPost]
        
        public JsonResult Story(int page_index)
        {
            long user_id = long.Parse(HttpContext.Session.GetString("UserId"));

            CIPlatform.Entitites.ViewModel.Mission stories = _allRepository.storyRepository.GetFileredStories(page_index, user_id);
            var next_stories = this.RenderViewAsync("story_partial", stories, true);
            return Json(new { next_stories });
        }

        [Route("/Story/ShareStory")]
        public IActionResult ShareStory()
        {
            if (HttpContext.Session.GetString("UserId") is not null)
            {
                long user_id = long.Parse(HttpContext.Session.GetString("UserId"));
                List<CIPlatform.Entitites.Models.Mission> missions = _allRepository.storyRepository.mission_of_user(user_id);
                return View(missions);
            }
            else
            {
                return RedirectToAction("Login", "UserAccount");
            }

        }

        [HttpPost]
        [Route("/Story/ShareStory")]
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
        [Route("Story/StoryDetails/{id}")]
        public IActionResult  StoryDetails(long id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "UserAccount", new { returnUrl = $"Story/StoryDetails/{id}" });
            }


            long user_id = long.Parse(HttpContext.Session.GetString("UserId"));
            CIPlatform.Entitites.ViewModel.StoryViewModel story = _allRepository.storyRepository.GetStory(user_id, id);
            if (story is not null)
            {
                _allRepository.storyRepository.Views(user_id, id);
                return View(story);
            }
            else
            {
                return View("page_not_found");
            }
        }

        [HttpPost]
        [Route("Story/StoryDetails/{id}")]
        public JsonResult RecommendCoWorker(string[] emailList, long Storyid)
        {
           


            long userId = long.Parse(HttpContext.Session.GetString("UserId"));

            if (emailList != null)
            {
                var mail = _allRepository.storyRepository.Recommend(emailList, Storyid, userId);
                _notyf.Success("Recommended Successfully...", 3);
                return Json(mail);
            }
            return Json(null);
        }
    }
}
