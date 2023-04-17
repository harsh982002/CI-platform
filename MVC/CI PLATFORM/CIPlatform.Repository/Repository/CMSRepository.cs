using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
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


        List<CIPlatform.Entitites.Models.Mission> missions = new List<Entitites.Models.Mission>();
        List<CIPlatform.Entitites.Models.MissionMedium> image = new List<Entitites.Models.MissionMedium>();
        List<MissionTheme> theme = new List<MissionTheme>();
        List<Country> countries = new List<Country>();
        List<City> cities = new List<City>();
        List<Skill> skills = new List<Skill>();
        List<MissionSkill> missionskills = new List<MissionSkill>();
        List<MissionDocument> mission_documents = new List<MissionDocument>();
        List<Comment> comments = new List<Comment>();
        List<User> users = new List<User>();
        List<MissionApplication> missionApplications = new List<MissionApplication>();
        List<FavoriteMission> favoriteMissions = new List<FavoriteMission>();
        List<MissionRating> ratings = new List<MissionRating>();
        List<MissionInvite> already_recommended_users = new List<MissionInvite>();
        List<Timesheet> timesheets = new List<Timesheet>();

        public CMSRepository(CiplatformContext db)
        {
            _db = db;
            getAllDetails();
        }

        private void getAllDetails()
        {

            missions = _db.Missions.ToList();
            image = _db.MissionMedia.ToList();
            theme = _db.MissionThemes.ToList();
            countries = _db.Countries.ToList();
            cities = _db.Cities.ToList();
            skills = _db.Skills.ToList();
            missionskills = _db.MissionSkills.ToList();
            mission_documents = _db.MissionDocuments.ToList();
            comments = _db.Comments.ToList();
            users = _db.Users.ToList();
            missionApplications = _db.MissionApplications.ToList();
            favoriteMissions = _db.FavoriteMissions.ToList();
            ratings = _db.MissionRatings.ToList();
            already_recommended_users = _db.MissionInvites.ToList();
            timesheets = _db.Timesheets.ToList();
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

        public Skill AddSkill(long user_id, SkillViewModel model)
        {
           Skill skill = new Skill();
            {
                skill.SkillName = model.SkillName;
                skill.Status = model.Status;
            }
            _db.Skills.Add(skill);
            _db.SaveChanges();
            return skill;
        }

        public Skill EditSkill(int skill_id, SkillViewModel model, string type)
        {
            Skill skill = _db.Skills.FirstOrDefault(x => x.SkillId == skill_id);
            if(skill is not null)
            {
                if(type == "edit-skill")
                {
                    skill.Status = model.Status;
                    skill.SkillName = model.SkillName;
                    _db.SaveChanges();
                    return skill;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public bool DeleteSkills(int skill_id)
        {
           Skill skills = _db.Skills.FirstOrDefault(x=>x.SkillId == skill_id);
            if(skills is null)
            {
                return false;
            }
            else
            {
                _db.MissionSkills.RemoveRange(_db.MissionSkills.Where(x=>x.SkillId == skills.SkillId));
                _db.UserSkills.RemoveRange(_db.UserSkills.Where(x=>x.SkillId ==skills.SkillId));
                _db.Skills.Remove(skills);
                _db.SaveChanges();
                return true;
            }
        }

        public bool deleteStory(long story_id)
        {
            Story story = _db.Stories.FirstOrDefault(x => x.StoryId == story_id);
            if(story is not null)
            {
                _db.StoryViews.RemoveRange(_db.StoryViews.Where(x => x.StoryId == story.StoryId));
                _db.StoryMedia.RemoveRange(_db.StoryMedia.Where(x => x.StoryId == story.StoryId));
                _db.StoryInvites.RemoveRange(_db.StoryInvites.Where(x => x.StoryId == story.StoryId));
                _db.Stories.Remove(story);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
