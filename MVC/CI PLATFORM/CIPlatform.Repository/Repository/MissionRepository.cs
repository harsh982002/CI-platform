using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class MissionRepository : Repository<CIPlatform.Entitites.Models.Mission>, IMissionRepository
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

        public MissionRepository(CiplatformContext db) : base(db)
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
          
        }

        public List<Entitites.ViewModel.Mission> GetAllMission()
        {
            

            var Missions = (from m in missions
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, themes = theme, skills = skills,TotalMission=missions.Count}).ToList();
            
            return Missions.Take(9).ToList();
           
        }

        public List<Entitites.ViewModel.Mission> GetFilteredMissions(List<string> Countries, List<string> Cities, List<string> Themes, List<string> Skills, string sort_by, int page_index)
        {
            List<CIPlatform.Entitites.ViewModel.Mission> Missions = new List<Entitites.ViewModel.Mission>();
            List<City> city = new List<City>();
            List<CIPlatform.Entitites.Models.Mission> mission = new List<CIPlatform.Entitites.Models.Mission>();

            if (page_index != 0)
            {
                missions = missions.Skip(9 * page_index).Take(9).ToList();
            }
            else
            {
                missions = missions.Take(9).ToList();
            }
            if (countries.Count > 0)
            {
                city = (from c in cities
                        where Countries.Contains(c.Country.Name)
                        select c).ToList();
            }
            else
            {
                city = cities;
            }
            if (Cities.Count > 0)
            {

                mission = (from m in missions
                           where Cities.Contains(m.City.Name) || Themes.Contains(m.Theme.Title)
                           select m).ToList();
                var skill_missions = (from s in missionskills
                                      where Skills.Contains(s.Skill.SkillName)
                                      select s.Mission).ToList();
                foreach (var skill_mission in skill_missions)
                {
                    if (!mission.Contains(skill_mission))
                    {
                        mission.Add(skill_mission);
                    }
                }
            }
            else if (Countries.Count > 0 || Themes.Count > 0 || Skills.Count > 0)
            {
                mission = (from m in missions
                           where Countries.Contains(m.Country.Name) || Cities.Contains(m.City.Name) || Themes.Contains(m.Theme.Title)
                           select m).ToList();
                var skill_missions = (from s in missionskills
                                      where Skills.Contains(s.Skill.SkillName)
                                      select s.Mission).ToList();
                foreach (var skill_mission in skill_missions)
                {
                    if (!mission.Contains(skill_mission))
                    {
                        mission.Add(skill_mission);
                    }
                }
            }
            else
            {
                mission = missions;
            }
            if (sort_by == "newest")
            {
                Missions = (from m in mission
                            orderby m.CreatedAt descending
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, Cities = city, Mission_city = m.City.Name, Mission_theme = m.Theme.Title }).ToList();
            }
            else if (sort_by == "oldest")
            {
                Missions = (from m in mission
                            orderby m.CreatedAt ascending
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, Cities = city, Mission_city = m.City.Name, Mission_theme = m.Theme.Title }).ToList();
            }
            else if (sort_by == "lowest available seats")
            {
                Missions = (from m in mission
                            orderby m.AvbSeat ascending
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, Cities = city, Mission_city = m.City.Name, Mission_theme = m.Theme.Title }).ToList();
            }
            else if (sort_by == "highest available seats")
            {
                Missions = (from m in mission
                            orderby m.AvbSeat descending
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, Cities = city, Mission_city = m.City.Name, Mission_theme = m.Theme.Title }).ToList();
            }
            else if (sort_by == "registration deadline")
            {

                Missions = (from m in mission
                            orderby m.Deadline ascending
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, Cities = city, Mission_city = m.City.Name, Mission_theme = m.Theme.Title }).ToList();
            }
            else
            {

                Missions = (from m in mission
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, Cities = city, Mission_city = m.City.Name, Mission_theme = m.Theme.Title }).ToList();
            }
            return Missions;
        }

        public List<Entitites.ViewModel.Mission> GetSearchMissions(string key, int page_index)
        {
            if (page_index != 0)
            {
                missions = missions.Skip(9 * page_index).Take(9).ToList();
            }
            else
            {
                missions = missions.Take(9).ToList();
            }

            var mission = (from m in missions
                           where m.Title.ToLower().Contains(key) || m.Description.ToLower().Contains(key)
                           select m).ToList();
            var Missions = (from m in mission
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Mission_city = m.City.Name, Mission_theme = m.Theme.Title }).ToList();
            return Missions.ToList();
        }

        public Entitites.Models.Mission Mission(long id)
        {
            CIPlatform.Entitites.Models.Mission? mission = _db.Missions.Find(id);
            return mission;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
