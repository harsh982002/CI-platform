﻿using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisioForge.MediaFramework.Helpers;

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
        List<Banner> banners1 = new List<Banner>();

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
            banners1 = _db.Banners.ToList();
        }

        /*MissionApplication*/
        public MissionAppViewModel GetApp()
        {
            MissionAppViewModel model = new MissionAppViewModel();
            List<CIPlatform.Entitites.Models.MissionApplication> missionApplications = _db.MissionApplications.ToList();

            List<CIPlatform.Entitites.ViewModel.MissionAppViewModel> allapplications = (from ma in missionApplications
                                                                                        orderby ma.AppliedAt ascending
                                                                                        select new MissionAppViewModel
                                                                                        {
                                                                                            id = ma.MissionApplicationId,
                                                                                            mission_id = ma.MissionId,
                                                                                            user_id = ma.UserId,
                                                                                            name = ma.User.FirstName + " " + ma.User.LastName,
                                                                                            Title = ma.Mission.Title,
                                                                                            AppliedAt = ma.AppliedAt,
                                                                                            ApprovalStatus = ma.ApprovalStatus,
                                                                                        }).ToList();
            model.MissionApps = allapplications;
            return model;
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
        public MissionSelectViewModel GetMission()
        {
            MissionSelectViewModel model = new MissionSelectViewModel();
            List<CIPlatform.Entitites.Models.Mission> missions = _db.Missions.ToList();
            List<CIPlatform.Entitites.ViewModel.MissionSelectViewModel> allmission = (from m in missions
                                                                                      select new MissionSelectViewModel
                                                                                      {
                                                                                          mission_id = m.MissionId,
                                                                                          missiontype = m.MissionType,
                                                                                          Title = m.Title,
                                                                                          StartDate = m.StartDate,
                                                                                          EndDate = m.EndDate,
                                                                                          Deadline = m.Deadline,


                                                                                      }).ToList();
            model.citys = _db.Cities.ToList();
            model.countries = _db.Countries.ToList();
            model.Skills = _db.Skills.ToList();
            model.theme = _db.MissionThemes.ToList();
            model.Missions = allmission;
            return model;
        }

        /*Story*/
        public StorySelectViewModel GetStory()
        {
            StorySelectViewModel model = new StorySelectViewModel();
            List<CIPlatform.Entitites.Models.Story> stories = _db.Stories.ToList();
            List<CIPlatform.Entitites.ViewModel.StorySelectViewModel> allstory = (from s in stories
                                                                                  select new StorySelectViewModel
                                                                                  {
                                                                                      StoryId = s.StoryId,
                                                                                      StoryName = s.Title,
                                                                                      UserName = s.User.FirstName + " " + s.User.LastName,
                                                                                      MissionId = s.MissionId,
                                                                                      MissionName = s.Mission.Title,
                                                                                      ApprovalStatus = s.Status

                                                                                  }).ToList();
            model.Stories = allstory;
            return model;
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
        public MissionThemeViewModel GetTheme()
        {
            MissionThemeViewModel model = new MissionThemeViewModel();
            List<CIPlatform.Entitites.Models.MissionTheme> themes = _db.MissionThemes.ToList();
            List<CIPlatform.Entitites.ViewModel.MissionThemeViewModel> allthemes = (from t in themes
                                                                                    select new MissionThemeViewModel
                                                                                    {
                                                                                        theme_id = t.MissionThemeId,
                                                                                        theme_name = t.Title,
                                                                                        status = t.Status,


                                                                                    }).ToList();
            model.MissionThemes = allthemes;
            return model;
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
            MissionTheme themename = _db.MissionThemes.FirstOrDefault(x => x.Title.ToLower() == model.theme_name.ToLower());
            if (themename == null)
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
            else
            {
                return null;
            }
        }

        /*Skills*/

        public SkillViewModel GetSkill()
        {
            SkillViewModel model = new SkillViewModel();
            List<CIPlatform.Entitites.Models.Skill> skills = _db.Skills.ToList();
            List<CIPlatform.Entitites.ViewModel.SkillViewModel> allskills = (from s in skills
                                                                             select new SkillViewModel
                                                                             {
                                                                                 SkillId = s.SkillId,
                                                                                 SkillName = s.SkillName,
                                                                                 Status = s.Status,
                                                                             }).ToList();
            model.SkillList = allskills;
            return model;
        }

        public Skill AddSkill(long user_id, SkillViewModel model)
        {
            Skill skilname = _db.Skills.Where(x => x.SkillName.ToLower() == model.SkillName).FirstOrDefault();
            if (skilname is null)
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
            else
            {
                return null;
            }

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
        public UserViewModel GetUser()
        {
            UserViewModel model = new UserViewModel();
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
            model.UserViewModels = allusers;
            return model;
        }

        public CmsViewModel GetCms()
        {
            CmsViewModel model = new CmsViewModel();
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
            model.cmsViewModel = allCms;
            return model;
        }

        public CmsViewModel GetAllCMS()
        {
            cmsPages = _db.CmsPages.ToList();
            var cms = new CmsViewModel { Title = cmsPages[0].Title, Slug = cmsPages.ElementAt(0).Slug, Description = cmsPages.ElementAt(0).Description, cmsPages = cmsPages };
            return cms;

        }

        public CmsPage AddCms(long user_id, CmsViewModel model)
        {
            CmsPage cms = _db.CmsPages.FirstOrDefault(x => x.Title.ToLower() == model.Title.ToLower() || x.Slug.ToLower() == model.Slug.ToLower());
            if (cms is null)
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
            else
            {
                return null;
            }

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
        public Boolean IsValidUserEmail(UserViewModel model)
        {
            try
            {
                User? user = _db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                if (user == null)
                {

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {

                return false;
            }
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

        public bool deletemission(long mission_id)
        {
            CIPlatform.Entitites.Models.Mission mission = _db.Missions.FirstOrDefault(x => x.MissionId == mission_id);
            if (mission is not null)
            {
                _db.Comments.RemoveRange(_db.Comments.Where(x => x.MissionId == mission.MissionId));
                _db.FavoriteMissions.RemoveRange(_db.FavoriteMissions.Where(x => x.MissionId == mission.MissionId));
                _db.MissionDocuments.RemoveRange(_db.MissionDocuments.Where(x => x.MissionId == mission.MissionId));
                _db.MissionInvites.RemoveRange(_db.MissionInvites.Where(x => x.MissionId == mission.MissionId));
                _db.MissionApplications.RemoveRange(_db.MissionApplications.Where(x => x.MissionId == mission.MissionId));
                _db.MissionMedia.RemoveRange(_db.MissionMedia.Where(x => x.MissionId == mission.MissionId));
                _db.MissionRatings.RemoveRange(_db.MissionRatings.Where(x => x.MissionId == mission.MissionId));
                _db.MissionSkills.RemoveRange(_db.MissionSkills.Where(x => x.MissionId == mission.MissionId));

                _db.Stories.RemoveRange(_db.Stories.Where(x => x.MissionId == mission.MissionId));
                _db.Timesheets.RemoveRange(_db.Timesheets.Where(x => x.MissionId == mission.MissionId));
                _db.GoalMissions.RemoveRange(_db.GoalMissions.Where(x => x.MissionId == mission.MissionId));
                _db.Missions.Remove(mission);
                _db.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool AddMission(MissionSelectViewModel model)
        {
            CIPlatform.Entitites.Models.Mission mission = new Entitites.Models.Mission();
            mission.CountryId = model.CountryId;
            mission.CityId = model.CityId;
            mission.ThemeId = model.ThemeId;
            mission.Title = model.Title;
            mission.Description = model.Description;
            mission.StartDate = model.StartDate;
            mission.EndDate = model.EndDate;
            mission.Deadline = model.Deadline;
            mission.OrganizationName = model.OrganizationName;
            mission.OrganizationDetail = model.OrganizationDetail;
            mission.MissionType = model.missiontype;
            mission.GoalObject = model.goalobject;
            mission.TotalSeats = model.TotalSeats;
            mission.Achieved = model.Achieved;
            mission.Status = "1";
            mission.AvbSeat = model.AvbSeat;
            mission.Availability = model.Availability;
            _db.Missions.Add(mission);
            _db.SaveChanges();
            string[]? getskills = model.skillname?.Split(",");
            foreach (var skill in getskills)
            {
                _db.MissionSkills.Add(new MissionSkill
                {
                    MissionId = mission.MissionId,
                    SkillId = int.Parse(skill)
                });
            }
            _db.SaveChanges();
            int count = 1;
            foreach (var image in model.missionMediums)
            {
                FileInfo fileInfo = new FileInfo(image.FileName);
                string filename = $"mission{mission.MissionId}-image-{count}" + fileInfo.Extension;
                string rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
                _db.MissionMedia.Add(new MissionMedium
                {
                    MissionId = mission.MissionId,
                    MediaPath = filename
                });
                using (Stream fileStream = new FileStream(rootpath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                count++;
            }
            _db.SaveChanges();
            foreach (var doc in model.MissionDocuments)
            {
                FileInfo fileInfo = new FileInfo(doc.FileName);
                string filename = $"mission{mission.MissionId}-document-{count}" + fileInfo.Extension;
                string rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", filename);
                _db.MissionDocuments.Add(new MissionDocument
                {
                    MissionId = mission.MissionId,
                    DocumentPath = filename
                });
                using (Stream fileStream = new FileStream(rootpath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
                count++;
            }
            _db.SaveChanges();
            return true;
        }

        public MissionSelectViewModel getdetails(long id)
        {
            CIPlatform.Entitites.Models.Mission mission = _db.Missions.Find(id);
            MissionSelectViewModel model = new MissionSelectViewModel();
            model.mission = new MissionSelectViewModel();
            model.mission.mission_id = id;
            model.mission.Achieved = mission?.Achieved;
            model.mission.Availability = mission?.Availability;
            model.mission.AvbSeat = mission?.AvbSeat;
            model.mission.CityId = mission.CityId;
            model.mission.CountryId = mission.CountryId;
            model.mission.Deadline = mission.Deadline;
            model.mission.Description = mission.Description;
            model.mission.ThemeId = mission.ThemeId;
            model.mission.StartDate = mission.StartDate;
            model.mission.EndDate = mission.EndDate;
            model.mission.Title = mission.Title;
            model.mission.TotalSeats = mission.TotalSeats;
            model.mission.goalobject = mission.GoalObject;
            model.mission.OrganizationName = mission.OrganizationName;
            model.mission.OrganizationDetail = mission.OrganizationDetail;
            model.mission.missiontype = mission.MissionType;
            model.citys = _db.Cities.ToList();
            model.countries = _db.Countries.ToList();
            model.Skills = _db.Skills.ToList();
            model.theme = _db.MissionThemes.ToList();
            return model;
        }

        public bool EditMission(long id, MissionSelectViewModel model)
        {
            CIPlatform.Entitites.Models.Mission updatemission = _db.Missions.FirstOrDefault(x => x.MissionId == id);

            if (updatemission is not null)
            {
                updatemission.Availability = model.Availability;
                updatemission.Achieved = model.Achieved;
                updatemission.AvbSeat = model.AvbSeat;
                updatemission.CountryId = model.CountryId;
                updatemission.CityId = model.CityId;
                updatemission.Deadline = model.Deadline;
                updatemission.EndDate = model.EndDate;
                updatemission.StartDate = model.StartDate;
                updatemission.ThemeId = model.ThemeId;
                updatemission.GoalObject = model.goalobject;
                updatemission.Title = model.Title;
                updatemission.Description = model.Description;
                updatemission.MissionType = model.missiontype;
                updatemission.OrganizationName = model.OrganizationName;
                updatemission.OrganizationDetail = model.OrganizationDetail;
                _db.SaveChanges();
                string[]? getskills = model.skillname?.Split(",");
                foreach (var skill in getskills)
                {
                    _db.MissionSkills.Add(new MissionSkill
                    {
                        MissionId = updatemission.MissionId,
                        SkillId = int.Parse(skill)
                    });
                }
                _db.SaveChanges();
                foreach (var image in updatemission.MissionMedia)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", image.MediaPath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    _db.MissionMedia.Remove(image);
                }
                int count = 1;
                foreach (var image in model.missionMediums)
                {
                    FileInfo fileInfo = new FileInfo(image.FileName);
                    string filename = $"mission{updatemission.MissionId}-image-{count}" + fileInfo.Extension;
                    string rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
                    _db.MissionMedia.Add(new MissionMedium
                    {
                        MissionId = updatemission.MissionId,
                        MediaPath = filename
                    });
                    using (Stream fileStream = new FileStream(rootpath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    count++;
                }
                _db.SaveChanges();
                foreach (var doc in updatemission.MissionDocuments)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", doc.DocumentPath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    _db.MissionDocuments.Remove(doc);
                }
                foreach (var doc in model.MissionDocuments)
                {
                    FileInfo fileInfo = new FileInfo(doc.FileName);
                    string filename = $"mission{updatemission.MissionId}-document-{count}" + fileInfo.Extension;
                    string rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", filename);
                    _db.MissionDocuments.Add(new MissionDocument
                    {
                        MissionId = updatemission.MissionId,
                        DocumentPath = filename
                    });
                    using (Stream fileStream = new FileStream(rootpath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    count++;
                }
                _db.SaveChanges();
                return true;

            }

            else
            {
                return false;
            }
        }

        public BannerViewModel GetBanner()
        {

            BannerViewModel model = new BannerViewModel();
            List<CIPlatform.Entitites.Models.Banner> banners = _db.Banners.ToList();
            List<CIPlatform.Entitites.ViewModel.BannerViewModel> allbanner = (from b in banners
                                                                              select new BannerViewModel
                                                                              {
                                                                                  BannerId = b.BannerId,
                                                                                  Text = b.Text,
                                                                                  Image = b.Image,
                                                                                  SortOrder = b.SortOrder,
                                                                              }).ToList();
            model.Bans = allbanner;
            return model;
        }

        public bool deletebanner(long banner_id)
        {
            Banner banner = _db.Banners.FirstOrDefault(x => x.BannerId == banner_id);
            if (banner is not null)
            {
                _db.Banners.Remove(banner);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Addbanner(BannerViewModel model)
        {
            CIPlatform.Entitites.Models.Banner banner = new Banner();
            {
                banner.Text = model.Text;
                banner.SortOrder = model.SortOrder;
                banner.Image = "abc";
            }
            _db.Banners.Add(banner);
            _db.SaveChanges();

            FileInfo fileInfo = new FileInfo(model.BannerImage.FileName);
            string filename = $"banner{banner.BannerId}-image" + fileInfo.Extension;
            string rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
            banner.Image = filename;
            using (Stream fileStream = new FileStream(rootpath, FileMode.Create))
            {
                model.BannerImage.CopyTo(fileStream);
            }


            _db.SaveChanges();
            return true;

        }

        public BannerViewModel getbannerdetail(long id)
        {
            Banner banner = _db.Banners.Find(id);
            BannerViewModel bannerViewModel = new BannerViewModel();
            bannerViewModel.BannerId = id;
            bannerViewModel.SortOrder = banner.SortOrder;
            bannerViewModel.Text = banner.Text;
            return bannerViewModel;
        }

        public bool editbanner(long id, BannerViewModel model)
        {
            Banner banner = _db.Banners.FirstOrDefault(x => x.BannerId == id);
            if (banner is not null)
            {
                banner.Text = model.Text;
                banner.SortOrder = model.SortOrder;

                FileInfo fileInfo = new FileInfo(model.BannerImage.FileName);
                string filename = $"banner{banner.BannerId}-image" + fileInfo.Extension;
                string rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
                banner.Image = filename;
                using (Stream fileStream = new FileStream(rootpath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }


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
