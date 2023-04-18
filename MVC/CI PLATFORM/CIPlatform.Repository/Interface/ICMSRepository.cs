using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface ICMSRepository
    {
        List<CIPlatform.Entitites.ViewModel.MissionAppViewModel> GetApp();
        bool updatestatus(long id, string? status);
        List<CIPlatform.Entitites.ViewModel.MissionSelectViewModel> GetMission();
        List<CIPlatform.Entitites.ViewModel.StorySelectViewModel> GetStory();
        bool deleteStory(long story_id);
        bool updatestorystatus(long story_id, string? status);
        List<CIPlatform.Entitites.ViewModel.MissionThemeViewModel> GetTheme();
        bool deletetheme(long theme_id);
        CIPlatform.Entitites.Models.MissionTheme EditTheme(long theme_id, CIPlatform.Entitites.ViewModel.MissionThemeViewModel model, string type);
        CIPlatform.Entitites.Models.MissionTheme AddTheme(long user_id, CIPlatform.Entitites.ViewModel.MissionThemeViewModel model);
        List<CIPlatform.Entitites.ViewModel.SkillViewModel> GetSkill();
        CIPlatform.Entitites.Models.Skill AddSkill(long user_id, CIPlatform.Entitites.ViewModel.SkillViewModel model);
       
        CIPlatform.Entitites.Models.Skill EditSkill(int skill_id, CIPlatform.Entitites.ViewModel.SkillViewModel model, string type);
        bool DeleteSkills(int skill_id);
        List<CIPlatform.Entitites.ViewModel.UserViewModel> GetUser();


        
        

    }
}
