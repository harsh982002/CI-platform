using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface ICMSRepository
    {
        CIPlatform.Entitites.ViewModel.MissionAppViewModel GetApp();
        bool updatestatus(long id, string? status);
        CIPlatform.Entitites.ViewModel.MissionSelectViewModel GetMission();

        bool deletemission(long mission_id);
        CIPlatform.Entitites.ViewModel.StorySelectViewModel GetStory();
        bool deleteStory(long story_id);
        bool updatestorystatus(long story_id, string? status);
        CIPlatform.Entitites.ViewModel.MissionThemeViewModel GetTheme();
        bool deletetheme(long theme_id);
        bool AddMission(CIPlatform.Entitites.ViewModel.MissionSelectViewModel model);
        CIPlatform.Entitites.Models.MissionTheme EditTheme(long theme_id, CIPlatform.Entitites.ViewModel.MissionThemeViewModel model, string type);
        CIPlatform.Entitites.Models.MissionTheme AddTheme(long user_id, CIPlatform.Entitites.ViewModel.MissionThemeViewModel model);
        CIPlatform.Entitites.ViewModel.SkillViewModel GetSkill();
        CIPlatform.Entitites.Models.Skill AddSkill(long user_id, CIPlatform.Entitites.ViewModel.SkillViewModel model);
       
        CIPlatform.Entitites.Models.Skill EditSkill(int skill_id, CIPlatform.Entitites.ViewModel.SkillViewModel model, string type);
        bool DeleteSkills(int skill_id);
        CIPlatform.Entitites.ViewModel.UserViewModel GetUser();

        bool deleteuser(long user_id);
        CIPlatform.Entitites.Models.User AddUser(CIPlatform.Entitites.ViewModel.UserViewModel model);

        CIPlatform.Entitites.Models.User EditUser(long user_id, CIPlatform.Entitites.ViewModel.UserViewModel model, string type);

        CIPlatform.Entitites.ViewModel.CmsViewModel GetCms();
        Entitites.ViewModel.CmsViewModel GetAllCMS();

        bool deletecms(long cms_id);
        CIPlatform.Entitites.Models.CmsPage AddCms(long user_id, CIPlatform.Entitites.ViewModel.CmsViewModel model);

        CIPlatform.Entitites.ViewModel.MissionSelectViewModel getdetails(long id);

        CIPlatform.Entitites.Models.CmsPage EditCms(long cms_id, CIPlatform.Entitites.ViewModel.CmsViewModel model, string type);

        bool EditMission(long id, CIPlatform.Entitites.ViewModel.MissionSelectViewModel model);

        CIPlatform.Entitites.ViewModel.BannerViewModel GetBanner();
    }
}
