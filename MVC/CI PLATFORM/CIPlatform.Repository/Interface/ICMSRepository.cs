using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface ICMSRepository
    {
        List<CIPlatform.Entitites.ViewModel.MissionSelectViewModel> GetMission();

        List<CIPlatform.Entitites.ViewModel.MissionAppViewModel> GetApp();

        List<CIPlatform.Entitites.ViewModel.MissionThemeViewModel> GetTheme();

        List<CIPlatform.Entitites.ViewModel.UserViewModel> GetUser();

        List<CIPlatform.Entitites.ViewModel.StorySelectViewModel> GetStory();

        List<CIPlatform.Entitites.ViewModel.SkillViewModel> GetSkill();

    }
}
