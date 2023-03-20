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
    public class VolunteerRepository : Repository<CIPlatform.Entitites.Models.Mission>, IVolunteerRepository
    {

        private readonly CiplatformContext _db;
        List<CIPlatform.Entitites.Models.Mission> missions = new List<Entitites.Models.Mission>();
        List<CIPlatform.Entitites.Models.MissionMedium> image = new List<MissionMedium>();
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
        public VolunteerRepository(CiplatformContext db) : base(db)
        {
            _db = db;
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
        public bool apply_for_mission(long UserId, long MissionId)
        {
            DateTime current = DateTime.Now;
            if (UserId != 0 && MissionId != 0)
            {
                var missionapplication = (from ma in missionApplications
                                          where ma.UserId.Equals(UserId) && ma.MissionId.Equals(MissionId)
                                          select ma).ToList();
                if (missionapplication.Count == 0)
                {
                    _db.MissionApplications.Add(new MissionApplication
                    {
                        AppliedAt = current,
                        UserId = UserId,
                        MissionId = MissionId
                    });
                    Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public VolunteerViewModel Missiondetails(int Id)
        {
            var missions = _db.Missions.FirstOrDefault(m => m.MissionId == Id);
            var cities = _db.Cities.ToList();
            var countries = _db.Countries.ToList();
            
            var themes = _db.MissionThemes.ToList();
           
            var skills = _db.Skills.ToList();
            var missionskills = _db.MissionSkills.ToList();
            var mission_documents = _db.MissionDocuments.ToList();
            var image = _db.MissionMedia.FirstOrDefault(x => x.MissionId == Id).MediaPath;
          
            VolunteerViewModel data = new VolunteerViewModel
            {
                Missions = missions, Cities = cities, Country = countries, themes = themes, skills = skills, Image = image 
            };
            return data;
        }

        public List<Entitites.ViewModel.Mission> GetAllMission()
        {


            var Missions = (from m in missions
                            join i in image on m.MissionId equals i.MissionId into data
                            from i in data.DefaultIfEmpty().Take(1)
                            select new CIPlatform.Entitites.ViewModel.Mission { image = i, Missions = m, Country = countries, themes = theme, skills = skills, TotalMission = missions.Count }).ToList();

            return Missions.ToList();

        }
        public List<VolunteerViewModel> GetAllMission(int Id)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public List<Entitites.Models.MissionRating> getMissionRatings(int Id)
        {
            List<MissionRating> rating = _db.MissionRatings.Where(a => a.MissionId == Id).ToList();
            return rating;
        }

        

        public bool AddFavouriteMission(long userId, long Id)
        {

            FavoriteMission favoriteMission = new FavoriteMission();
            favoriteMission.UserId = userId;
            favoriteMission.MissionId = Id;

            var favmission = _db.FavoriteMissions.FirstOrDefault(s => s.UserId == userId && s.MissionId == Id);
            if (favmission == null)
            {
                _db.FavoriteMissions.Add(favoriteMission);
                _db.SaveChanges();
                return true;
            }
            else
            {
                _db.FavoriteMissions.Remove(favmission);
                _db.SaveChanges();
                return false;
            }
        }

     
        private void Save()
        {
            throw new NotImplementedException();
        }
    }
}
