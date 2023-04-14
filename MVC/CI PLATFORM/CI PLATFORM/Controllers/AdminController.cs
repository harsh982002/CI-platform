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

        [Route("/Admin/Skill")]
        public IActionResult Skill_CMS()
        {
            List<CIPlatform.Entitites.ViewModel.SkillViewModel> skills = _allRepository.cmsRepository.GetSkill();
            return View(skills);
        }
    }
}
