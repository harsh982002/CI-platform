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
            return View();
        }

        [Route("/Admin")]
        public IActionResult User_CMS()
        {
            List<CIPlatform.Entitites.ViewModel.UserViewModel> users = _allRepository.cmsRepository.GetUser();
            return View(users);
        }

        [Route("/Admin/Theme")]
        public IActionResult Mission_Theme_CMS()
        {
            List<CIPlatform.Entitites.ViewModel.MissionThemeViewModel> themes = _allRepository.cmsRepository.GetTheme();
            return View(themes);
        }

        [Route("/Admin/App")]
        public IActionResult Mission_Application_CMS()
        {
            List<CIPlatform.Entitites.ViewModel.MissionAppViewModel> applications = _allRepository.cmsRepository.GetApp();
            return View(applications);
        }

        
        [Route("/Admin/Mission")]
        public IActionResult Mission_CMS()
        {
            List<CIPlatform.Entitites.ViewModel.MissionSelectViewModel> missions = _allRepository.cmsRepository.GetMission();
            return View(missions);
        }
        
        [Route("/Admin/Story")]
        public IActionResult Story_CMS()
        {
            List<CIPlatform.Entitites.ViewModel.StorySelectViewModel> stories = _allRepository.cmsRepository.GetStory();
            return View(stories);
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
            List<CIPlatform.Entitites.ViewModel.SkillViewModel> skills = _allRepository.cmsRepository.GetSkill();
            return View(skills);
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
