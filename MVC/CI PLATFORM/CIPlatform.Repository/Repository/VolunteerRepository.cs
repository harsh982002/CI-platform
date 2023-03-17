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
    public class VolunteerRepository : Repository<CIPlatform.Entitites.Models.Mission>, IVolunteerRepository
    {

        private readonly CiplatformContext _db;
        public VolunteerRepository(CiplatformContext db) : base(db)
        {
            _db = db;
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

        public List<VolunteerViewModel> GetAllMission(int Id)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }
    }
}
