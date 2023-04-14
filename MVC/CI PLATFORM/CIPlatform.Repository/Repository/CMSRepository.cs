using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class CMSRepository : ICMSRepository
    {
        private readonly CiplatformContext _db;

        public CMSRepository(CiplatformContext db)
        {
            _db = db;
        }

        public List<MissionAppViewModel> GetApp()
        {
            List<CIPlatform.Entitites.Models.MissionApplication> missionApplications = _db.MissionApplications.ToList();
            List<CIPlatform.Entitites.ViewModel.MissionAppViewModel> allapplications = (from ma in missionApplications
                                                                                        select new MissionAppViewModel
                                                                                        {
                                                                                            mission_id = ma.MissionId,
                                                                                            user_id = ma.UserId,
                                                                                            name = ma.User.FirstName + " " + ma.User.LastName,
                                                                                            Title = ma.Mission.Title,
                                                                                            AppliedAt = ma.AppliedAt,
                                                                                        }).ToList();
            return allapplications;
        }

        public List<MissionSelectViewModel> GetMission()
        {
            List<CIPlatform.Entitites.Models.Mission> missions = _db.Missions.ToList();
            List<CIPlatform.Entitites.ViewModel.MissionSelectViewModel> allmission = (from m in missions
                                                                                      select new MissionSelectViewModel
                                                                                      {
                                                                                            mission_id = m.MissionId,
                                                                                            missiontype = m.MissionType,
                                                                                            Title = m.Title,
                                                                                            StartDate = m.StartDate,
                                                                                            EndDate = m.EndDate,

                                                                                      }).ToList();
         

            return allmission;
        }

        public List<SkillViewModel> GetSkill()
        {
            List<CIPlatform.Entitites.Models.Skill> skills = _db.Skills.ToList();
            List<CIPlatform.Entitites.ViewModel.SkillViewModel> allskills = (from s in skills
                                                                             select new SkillViewModel
                                                                             {
                                                                                 SkillId = s.SkillId,
                                                                                 SkillName = s.SkillName,
                                                                                 Status = s.Status,
                                                                             }).ToList();
            return allskills;
        }

        public List<StorySelectViewModel> GetStory()
        {
            List<CIPlatform.Entitites.Models.Story> stories = _db.Stories.ToList();
            List<CIPlatform.Entitites.ViewModel.StorySelectViewModel> allstory = (from s in stories
                                                                                  select new StorySelectViewModel
                                                                                  {
                                                                                      StoryId = s.StoryId,
                                                                                      StoryName = s.Title,
                                                                                      UserName = s.User.FirstName + " " + s.User.LastName,
                                                                                      MissionId = s.MissionId,
                                                                                      MissionName = s.Mission.Title,
                                                                                  }).ToList();
            return allstory;
        }

        public List<MissionThemeViewModel> GetTheme()
        {
            List<CIPlatform.Entitites.Models.MissionTheme> themes = _db.MissionThemes.ToList();
            List<CIPlatform.Entitites.ViewModel.MissionThemeViewModel> allthemes = (from t in themes
                                                                                    select new MissionThemeViewModel
                                                                                    {
                                                                                        theme_id = t.MissionThemeId,
                                                                                        theme_name = t.Title,
                                                                                        status = t.Status,
                                                                                        
                                                                                        
                                                                                    }).ToList();
            return allthemes;
        }

        public List<UserViewModel> GetUser()
        {
            List<CIPlatform.Entitites.Models.User> users = _db.Users.ToList();
            List<CIPlatform.Entitites.ViewModel.UserViewModel> allusers = (from u in users
                                                                           select new UserViewModel
                                                                           {
                                                                               user_id = u.UserId,
                                                                               FirstName = u.FirstName,
                                                                               LastName = u.LastName,
                                                                               Email = u.Email,
                                                                               EmpId = u.EmployeeId,
                                                                               Department = u.Department,
                                                                               status = u.Status,
                                                                           }).ToList();
            return allusers;
        }
    }
}
