using CIPlatform.Entitites.Models;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAllRepository _allRepository;

        public AdminController(IAllRepository allRepository)
        {
            _allRepository = allRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/CMS")]
        public IActionResult CMS()
        {
            if(HttpContext.Session.GetString("role") is  null) {
                return RedirectToAction("Landingplatform", "Home");
              
            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.CmsViewModel> cms = _allRepository.cmsRepository.GetCms();
                return View(cms);
            }
       
        }
        [HttpPost]
        [Route("/Admin/CMS")]
        public IActionResult CMS(long cms_id,string? title, string? editor, string? slug, string? status,string? type)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            if(type == "cms-delete")
            {
                bool success = _allRepository.cmsRepository.deletecms(cms_id);
                return Json(new { success });
            }
            else if(type == "edit-cms")
            {
                CIPlatform.Entitites.ViewModel.CmsViewModel model = new CIPlatform.Entitites.ViewModel.CmsViewModel
                {
                    Title = title,
                    Description = editor,
                    Slug = slug,
                    Status = status,
                };
                CmsPage cms = _allRepository.cmsRepository.EditCms(cms_id, model, type);
                return View(cms);
            }
            else
            {
                CIPlatform.Entitites.ViewModel.CmsViewModel model = new CIPlatform.Entitites.ViewModel.CmsViewModel
                {
                    Title = title,
                    Description = editor,
                    Slug = slug,
                    Status = status,
                };
                CmsPage cms = _allRepository.cmsRepository.AddCms(userId, model);
                return View(cms);
            }
        }

        [Route("/Admin")]
        public IActionResult User_CMS()
        {
            if (HttpContext.Session.GetString("role") is  null)
            {
                return RedirectToAction("Landingplatform", "Home");
               
            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.UserViewModel> users = _allRepository.cmsRepository.GetUser();
                return View(users);
            }
        }

        [HttpPost]
        [Route("/Admin")]
        public IActionResult User_CMS(long user_id,string? type)
        {
            if (type == "user-delete")
            {
                bool success = _allRepository.cmsRepository.deleteuser(user_id);
                return Json(new { success });
            }
            else
            {
                return null;
            }
        }
     
        [Route("/Admin/Theme")]
        public IActionResult Mission_Theme_CMS()
        {
            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.MissionThemeViewModel> themes = _allRepository.cmsRepository.GetTheme();
                return View(themes);
            }
        }
        [HttpPost]
        [Route("/Admin/Theme")]
        public IActionResult Mission_Theme_CMS(long theme_id, string type, string? theme, byte? themestatus)
        {
           if(type == "theme-delete")
            {
                bool success = _allRepository.cmsRepository.deletetheme(theme_id);
                return Json(new { success });
            }
           else if(type == "edit-theme")
            {
                CIPlatform.Entitites.ViewModel.MissionThemeViewModel model = new CIPlatform.Entitites.ViewModel.MissionThemeViewModel
                {
                    status = themestatus,
                    theme_name = theme,
                };
                MissionTheme themes = _allRepository.cmsRepository.EditTheme(theme_id, model, type);
                return View(themes);
            }
            else
            {

                CIPlatform.Entitites.ViewModel.MissionThemeViewModel model = new CIPlatform.Entitites.ViewModel.MissionThemeViewModel
                {
                    status = themestatus,
                    theme_name = theme,
                };
                MissionTheme themes = _allRepository.cmsRepository.AddTheme(theme_id, model);
                return View(themes);
            }
        }
        [Route("/Admin/App")]
        public IActionResult Mission_Application_CMS()
        {
            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.MissionAppViewModel> applications = _allRepository.cmsRepository.GetApp();
                return View(applications);
            }
           
        }

        [Route("/Admin/MAValidate")]
        public IActionResult Ma_Validate(long ma_id, string? status)
        {
            bool success = _allRepository.cmsRepository.updatestatus(ma_id, status);
            return Json(new { success });
        }


        [Route("/Admin/Mission")]
        public IActionResult Mission_CMS()
        {
            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.MissionSelectViewModel> missions = _allRepository.cmsRepository.GetMission();
                return View(missions);
            }
        }


        [Route("/Admin/Story")]
        public IActionResult Story_CMS()
        {

            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.StorySelectViewModel> stories = _allRepository.cmsRepository.GetStory();
                return View(stories);
            }
        }

        [Route("/Admin/storyvalidate")]
        public IActionResult story_validate(long story_id,string? status)
        {
            bool success = _allRepository.cmsRepository.updatestorystatus(story_id, status);
            return Json(new { success });
        }


        [HttpPost]
        [Route("/Admin/Story")]
        public IActionResult Story_CMS(long story_id,string? type)
        {
            if(type == "story-delete")
            {
                bool success = _allRepository.cmsRepository.deleteStory(story_id);
                return Json(new { success });
            }
            else
            {
                return null;
            }
        }

        [Route("/Admin/Skill")]
        public IActionResult Skill_CMS()
        {

            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                List<CIPlatform.Entitites.ViewModel.SkillViewModel> skills = _allRepository.cmsRepository.GetSkill();
                return View(skills);
            }
        }

        [HttpPost]
        [Route("/Admin/Skill")]
        public IActionResult Skill_CMS(int skill_id, string type,string? sname, byte? status)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            if (type == "skill-delete")
            {
                bool success = _allRepository.cmsRepository.DeleteSkills(skill_id);
                return Json(new { success });
            }
            else if(type == "edit-skill")
            {
                CIPlatform.Entitites.ViewModel.SkillViewModel model = new CIPlatform.Entitites.ViewModel.SkillViewModel
                {
                    SkillName = sname,
                    Status = status
                };
                Skill skill = _allRepository.cmsRepository.EditSkill(skill_id,model,type);
                return View(skill);
            }
            else
            {
                CIPlatform.Entitites.ViewModel.SkillViewModel model = new CIPlatform.Entitites.ViewModel.SkillViewModel
                {
                    SkillName = sname,
                    Status = status
                };
                Skill skill = _allRepository.cmsRepository.AddSkill(userId, model);
                return View(skill);
            }
        }
    }
}
