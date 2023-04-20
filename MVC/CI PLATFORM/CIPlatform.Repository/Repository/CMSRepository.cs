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
        List<CmsPage> cmsPages = new List<CmsPage>();

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
            cmsPages = _db.CmsPages.ToList();
        }

        /*MissionApplication*/
        public List<MissionAppViewModel> GetApp()
        {
            List<CIPlatform.Entitites.Models.MissionApplication> missionApplications = _db.MissionApplications.ToList();
            List<CIPlatform.Entitites.ViewModel.MissionAppViewModel> allapplications = (from ma in missionApplications
                                                                                        select new MissionAppViewModel
                                                                                        {
                                                                                            id = ma.MissionApplicationId,
                                                                                            mission_id = ma.MissionId,
                                                                                            user_id = ma.UserId,
                                                                                            name = ma.User.FirstName + " " + ma.User.LastName,
                                                                                            Title = ma.Mission.Title,
                                                                                            AppliedAt = ma.AppliedAt,
                                                                                        }).ToList();
            return allapplications;
        }
        public bool updatestatus(long id, string? status)
        {
            MissionApplication ma = _db.MissionApplications.FirstOrDefault(x => x.MissionApplicationId == id);
            if (ma != null)
            {
                if (status == "APPROVE")
                {
                    if (ma.Mission.AvbSeat > 0)
                    {
                        ma.Mission.AvbSeat = ma.Mission.AvbSeat - 1;
                    }
                }
                ma.ApprovalStatus = status;
                _db.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }

        /*Mission*/
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

        /*Story*/
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

        public bool deleteStory(long story_id)
        {
            Story story = _db.Stories.FirstOrDefault(x => x.StoryId == story_id);
            if (story is not null)
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

        public bool updatestorystatus(long story_id, string? status)
        {
            Story story = _db.Stories.FirstOrDefault(x => x.StoryId == story_id);
            if (story is not null)
            {
                if (status == "DECLINED")
                {
                    story.Status = "DECLINED";
                }
                else
                {
                    story.Status = story.StatusUserwant;
                }

                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*Themes*/
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

        public bool deletetheme(long theme_id)
        {
            MissionTheme themes = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == theme_id);
            if (themes is not null)
            {
                _db.MissionThemes.Remove(themes);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public MissionTheme EditTheme(long theme_id, MissionThemeViewModel model, string type)
        {
            MissionTheme missionTheme = _db.MissionThemes.FirstOrDefault(x => x.MissionThemeId == theme_id);
            if (type == "edit-theme")
            {
                if (missionTheme != null)
                {
                    missionTheme.Title = model.theme_name;
                    missionTheme.Status = model.status;
                    _db.SaveChanges();
                    return missionTheme;
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

        public MissionTheme AddTheme(long user_id, MissionThemeViewModel model)
        {
            MissionTheme theme = new MissionTheme();
            {
                theme.Title = model.theme_name;
                theme.Status = model.status;
            };
            _db.MissionThemes.Add(theme);
            _db.SaveChanges();
            return theme;
        }

        /*Skills*/

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
            if (skill is not null)
            {
                if (type == "edit-skill")
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
            Skill skills = _db.Skills.FirstOrDefault(x => x.SkillId == skill_id);
            if (skills is null)
            {
                return false;
            }
            else
            {
                _db.MissionSkills.RemoveRange(_db.MissionSkills.Where(x => x.SkillId == skills.SkillId));
                _db.UserSkills.RemoveRange(_db.UserSkills.Where(x => x.SkillId == skills.SkillId));
                _db.Skills.Remove(skills);
                _db.SaveChanges();
                return true;
            }
        }

        /*User*/
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
                                                                               Password = u.Password,
                                                                               PhoneNumber = u.PhoneNumber,
                                                                               Department = u.Department,
                                                                               Role = u.Role,
                                                                               status = u.Status,
                                                                               CityId = u.CityId,
                                                                               CountryId = u.CountryId,
                                                                               ProfileText = u.ProfileText,
                                                                               Avatar = u.Avatar,
                                                                           }).ToList();
            return allusers;
        }

        public List<CmsViewModel> GetCms()
        {
            List<CIPlatform.Entitites.Models.CmsPage> cmsPages = _db.CmsPages.ToList();
            List<CIPlatform.Entitites.ViewModel.CmsViewModel> allCms = (from c in cmsPages
                                                                        select new CmsViewModel
                                                                        {
                                                                            CmsPageId = c.CmsPageId,
                                                                            Description = c.Description,
                                                                            Slug = c.Slug,
                                                                            Status = c.Status,
                                                                            Title = c.Title,
                                                                        }).ToList();
            return allCms;
        }

        public CmsViewModel GetAllCMS()
        {
            cmsPages = _db.CmsPages.ToList();
            var cms = new CmsViewModel { Title = cmsPages[0].Title, Slug = cmsPages.ElementAt(0).Slug, Description = cmsPages.ElementAt(0).Description, cmsPages = cmsPages };
            return cms;

        }

        public CmsPage AddCms(long user_id, CmsViewModel model)
        {
            CmsPage cmsPage = new CmsPage();
            {
                cmsPage.Title = model.Title;
                cmsPage.Slug = model.Slug;
                cmsPage.Status = model.Status;
                cmsPage.Description = model.Description;
            }
            _db.CmsPages.Add(cmsPage);
            _db.SaveChanges();
            return cmsPage;
        }

        public bool deletecms(long cms_id)
        {
            CmsPage cmsPage = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == cms_id);
            if (cmsPage is null)
            {
                return false;

            }
            else
            {
                _db.CmsPages.Remove(cmsPage);
                _db.SaveChanges();
                return true;
            }
        }

        public CmsPage EditCms(long cms_id, CmsViewModel model, string type)
        {
            CmsPage cmsPage = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == cms_id);
            if (cmsPage != null)
            {
                if (type == "edit-cms")
                {
                    cmsPage.Title = model.Title;
                    cmsPage.Description = model.Description;
                    cmsPage.Status = model.Status;
                    cmsPage.Slug = model.Slug;
                    _db.SaveChanges();
                    return cmsPage;
                }
                else { return null; }
            }
            else
            {
                return null;
            }
        }

        public bool deleteuser(long user_id)
        {
            User user = _db.Users.FirstOrDefault(x => x.UserId == user_id);
            if (user is null)
            {
                return false;
            }
            else
            {
                _db.Comments.RemoveRange(_db.Comments.Where(x => x.UserId == user.UserId));
                _db.FavoriteMissions.RemoveRange(_db.FavoriteMissions.Where(x => x.UserId == user.UserId));
                _db.MissionApplications.RemoveRange(_db.MissionApplications.Where(x => x.UserId == user.UserId));
                _db.MissionInvites.RemoveRange(_db.MissionInvites.Where(x => x.FromUserId == user.UserId || x.FromUserId == user.UserId));
                _db.MissionRatings.RemoveRange(_db.MissionRatings.Where(x => x.UserId == user.UserId));
                _db.Stories.RemoveRange(_db.Stories.Where(x => x.UserId == user.UserId));
                _db.StoryViews.RemoveRange(_db.StoryViews.Where(x => x.UserId == user.UserId));
                _db.StoryInvites.RemoveRange(_db.StoryInvites.Where(x => x.FromUserId == user.UserId || x.FromUserId == user.UserId));
                _db.Timesheets.RemoveRange(_db.Timesheets.Where(x => x.UserId == user.UserId));
                _db.UserSkills.RemoveRange(_db.UserSkills.Where(x => x.UserId == user.UserId));
                _db.Users.Remove(user);
                _db.SaveChanges();
                return true;
            }
        }

        public User AddUser(UserViewModel model)
        {
            string secpass = BCrypt.Net.BCrypt.HashPassword(model.Password);
            User user = new User();
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Password = secpass;
                user.PhoneNumber = model.PhoneNumber;
                user.Role = model.Role;
            }
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User EditUser(long user_id, UserViewModel model, string type)
        {
            User user = _db.Users.FirstOrDefault(x => x.UserId == user_id);
            if (user is not null)
            {
                if (type == "edit-user")
                {
                    user.EmployeeId = model.EmpId;
                    user.Role = model.Role;
                    user.Department = model.Department;
                    user.Status = model.status;
                    _db.SaveChanges();
                    return user;
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
    }
}
