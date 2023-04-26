using AspNetCoreHero.ToastNotification.Abstractions;
using CIPlatform.Entitites.Models;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAllRepository _allRepository;
        private readonly INotyfService _notyf;
       
        public AdminController(IAllRepository allRepository, INotyfService notyf)
        {
            _allRepository = allRepository;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/CMS")]
        public IActionResult CMS()
        {
            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                CIPlatform.Entitites.ViewModel.CmsViewModel cms = _allRepository.cmsRepository.GetCms();
                return View(cms);
            }

        }
        [HttpPost]
        [Route("/Admin/CMS")]
        public IActionResult CMS(long cms_id, string? title, string? editor, string? slug, string? status, string? type)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            if (type == "cms-delete")
            {
                bool success = _allRepository.cmsRepository.deletecms(cms_id);
                return Json(new { success });
            }
            else if (type == "edit-cms")
            {
                CIPlatform.Entitites.ViewModel.CmsViewModel model = new CIPlatform.Entitites.ViewModel.CmsViewModel
                {
                    Title = title,
                    Description = editor,
                    Slug = slug,
                    Status = status,
                };
                CmsPage cms = _allRepository.cmsRepository.EditCms(cms_id, model, type);
                bool success = false;
                if(cms != null)
                {
                    success = true;
                }
                return Json(new {success});
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
                bool success = false;
                if (cms != null)
                {
                    success = true;
                }
                return Json(new { success }); ;
            }
        }

        [Route("/Admin")]
        public IActionResult User_CMS()
        {
            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                CIPlatform.Entitites.ViewModel.UserViewModel users = _allRepository.cmsRepository.GetUser();
                return View(users);
            }
        }

        [HttpPost]
        [Route("/Admin")]
        public IActionResult User_CMS(long user_id, string? type, string? fname, string? lname, string? phone, string? email, string? pass, string? role, string? empid, string? department, string? status, CIPlatform.Entitites.ViewModel.UserViewModel model1)
        {

            if (type == "user-delete")
            {
                bool success = _allRepository.cmsRepository.deleteuser(user_id);
                return Json(new { success });
            }
            else if (type == "edit-user")
            {
                CIPlatform.Entitites.ViewModel.UserViewModel model = new CIPlatform.Entitites.ViewModel.UserViewModel
                {
                    Role = role,
                    EmpId = empid,
                    Department = department,
                    status = status
                };
                User user = _allRepository.cmsRepository.EditUser(user_id, model, type);
                bool success = false;
                if(user != null)
                {
                    success = true;
                }
                return Json(new {success});
            }
            else
            {
                if (_allRepository.cmsRepository.IsValidUserEmail(model1))
                        {
                    CIPlatform.Entitites.ViewModel.UserViewModel model = new CIPlatform.Entitites.ViewModel.UserViewModel
                    {
                        FirstName = fname,
                        LastName = lname,
                        Email = email,
                        Password = pass,
                        Role = role,
                        PhoneNumber = phone

                    };
                    User user = _allRepository.cmsRepository.AddUser(model);
                    bool success = false;
                    if(user is not null)
                    {
                        success = true;
                    }
                    return Json(new {success});
                }
                else
                {
                    _notyf.Warning("This Mail Account Already Register !! Please Check your mail or Login your Account...");
                   /* ViewBag.alert = String.Format("This Mail Account Already Register !! Please Check your mail or Login your Account...");*/
                    CIPlatform.Entitites.ViewModel.UserViewModel users = _allRepository.cmsRepository.GetUser();
                    return View("User_CMS",users);
                }
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
                CIPlatform.Entitites.ViewModel.MissionThemeViewModel themes = _allRepository.cmsRepository.GetTheme();
                return View(themes);
            }
        }
        [HttpPost]
        [Route("/Admin/Theme")]
        public IActionResult Mission_Theme_CMS(long theme_id, string type, string? theme, byte? themestatus)
        {
            if (type == "theme-delete")
            {
                bool success = _allRepository.cmsRepository.deletetheme(theme_id);
                return Json(new { success });
            }
            else if (type == "edit-theme")
            {
                CIPlatform.Entitites.ViewModel.MissionThemeViewModel model = new CIPlatform.Entitites.ViewModel.MissionThemeViewModel
                {
                    status = themestatus,
                    theme_name = theme,
                };
                MissionTheme themes = _allRepository.cmsRepository.EditTheme(theme_id, model, type);
                bool success = false;
                if(themes is not null)
                {
                    success = true;
                }
                return Json(new {success});
            }
            else
            {

                CIPlatform.Entitites.ViewModel.MissionThemeViewModel model = new CIPlatform.Entitites.ViewModel.MissionThemeViewModel
                {
                    status = themestatus,
                    theme_name = theme,
                };
                MissionTheme themes = _allRepository.cmsRepository.AddTheme(theme_id, model);
                bool success = false;
                if(themes is not null)
                {
                    success = true;
                }
                return Json(new {success});
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
                CIPlatform.Entitites.ViewModel.MissionAppViewModel applications = _allRepository.cmsRepository.GetApp();
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
                CIPlatform.Entitites.ViewModel.MissionSelectViewModel missions = _allRepository.cmsRepository.GetMission();
                return View(missions);
            }
        }

        [HttpPost]
        [Route("/Admin/Mission")]
        public IActionResult Mission_CMS(string? type, long mission_id)
        {
            if (type == "mission-delete")
            {
                bool success = _allRepository.cmsRepository.deletemission(mission_id);
                return Json(new { success });
            }
            else
            {
                return null;
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
                CIPlatform.Entitites.ViewModel.StorySelectViewModel stories = _allRepository.cmsRepository.GetStory();
                return View(stories);
            }
        }

        [Route("/Admin/storyvalidate")]
        public IActionResult story_validate(long story_id, string? status)
        {
            bool success = _allRepository.cmsRepository.updatestorystatus(story_id, status);
            return Json(new { success });
        }


        [HttpPost]
        [Route("/Admin/Story")]
        public IActionResult Story_CMS(long story_id, string? type)
        {
            if (type == "story-delete")
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
                CIPlatform.Entitites.ViewModel.SkillViewModel skills = _allRepository.cmsRepository.GetSkill();
                return View(skills);
            }
        }

        [HttpPost]
        [Route("/Admin/Skill")]
        public IActionResult Skill_CMS(int skill_id, string type, string? sname, byte? status)
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));
            if (type == "skill-delete")
            {
                bool success = _allRepository.cmsRepository.DeleteSkills(skill_id);
                return Json(new { success });
            }
            else if (type == "edit-skill")
            {
                CIPlatform.Entitites.ViewModel.SkillViewModel model = new CIPlatform.Entitites.ViewModel.SkillViewModel
                {
                    SkillName = sname,
                    Status = status
                };
                Skill skill = _allRepository.cmsRepository.EditSkill(skill_id, model, type);
                bool success = false;
                if(skill != null)
                {
                    success = true;
                }
                return Json(new {success});
            }
            else
            {
                CIPlatform.Entitites.ViewModel.SkillViewModel model = new CIPlatform.Entitites.ViewModel.SkillViewModel
                {
                    SkillName = sname,
                    Status = status
                };
                Skill skill = _allRepository.cmsRepository.AddSkill(userId, model);
                bool success = false;
                if (skill != null)
                {
                    success = true;
                }
                return Json(new { success });
                
            }
        }

        [Route("/Admin/AddMission")]
        public IActionResult AddMission(CIPlatform.Entitites.ViewModel.MissionSelectViewModel model)
        {
            if (model.mission is null)
            {
                CIPlatform.Entitites.ViewModel.MissionSelectViewModel missions = _allRepository.cmsRepository.GetMission();
                return View(missions);
            }
            else if(model.mission.mission_id != 0)
            {

                bool success = _allRepository.cmsRepository.EditMission(model.mission.mission_id, model.mission);
                CIPlatform.Entitites.ViewModel.MissionSelectViewModel mis = _allRepository.cmsRepository.GetMission();
                return View("Mission_CMS", mis);
            }
            else
            {
                bool success = _allRepository.cmsRepository.AddMission(model.mission);
                CIPlatform.Entitites.ViewModel.MissionSelectViewModel mis = _allRepository.cmsRepository.GetMission();
                return View("Mission_CMS", mis);
            }

        }
        [Route("/Admin/EditMission/{mission_id}")]
        public IActionResult EditMission(long mission_id, CIPlatform.Entitites.ViewModel.MissionSelectViewModel model, string? type)
        {

            CIPlatform.Entitites.ViewModel.MissionSelectViewModel missions = _allRepository.cmsRepository.getdetails(mission_id);
            return View("AddMission", missions);
            

        }

        [Route("/Admin/Banner")]
        public IActionResult Banner()
        {
            if (HttpContext.Session.GetString("role") is null)
            {
                return RedirectToAction("Landingplatform", "Home");

            }
            else
            {
                CIPlatform.Entitites.ViewModel.BannerViewModel banner = _allRepository.cmsRepository.GetBanner();
                return View(banner); ;
            }

        }

        [HttpPost]
        [Route("/Admin/Banner")]
        public IActionResult Banner(long banner_id, string? type )
        {
            if (type == "banner-delete")
            {
                bool success = _allRepository.cmsRepository.deletebanner(banner_id);
                return Json(new{success});
            }
            else
            {
                return null;
            }
        }

        [Route("/Admin/AddBanner")]
        public IActionResult AddBanner()
        {
            
                CIPlatform.Entitites.ViewModel.BannerViewModel banner = _allRepository.cmsRepository.GetBanner();
                return View(banner);
            
        }
        [HttpPost]
        [Route("/Admin/AddBanner")]
        public IActionResult AddBanner(CIPlatform.Entitites.ViewModel.BannerViewModel model)
        {
            if (model == null)
            {
                CIPlatform.Entitites.ViewModel.BannerViewModel banner = _allRepository.cmsRepository.GetBanner();
                return View(banner);
            }
            else if(model.BannerId != 0)
            {
                bool success = _allRepository.cmsRepository.editbanner(model.BannerId, model);
                CIPlatform.Entitites.ViewModel.BannerViewModel mis = _allRepository.cmsRepository.GetBanner();
                return View("Banner", mis);
            }
            else
            {
                bool success = _allRepository.cmsRepository.Addbanner(model);
                CIPlatform.Entitites.ViewModel.BannerViewModel mis = _allRepository.cmsRepository.GetBanner();
                return View("Banner", mis);
            }
        }

        [Route("/Admin/EditBanner/{banner_id}")]
        public IActionResult EditBanner(long banner_id)
        {
            CIPlatform.Entitites.ViewModel.BannerViewModel ban = _allRepository.cmsRepository.getbannerdetail(banner_id);
            return View("AddBanner", ban);
        }
        
    }
}
