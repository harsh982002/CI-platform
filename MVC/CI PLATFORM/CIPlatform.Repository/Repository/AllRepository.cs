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
    public class AllRepository : IAllRepository
    {
        private readonly CiplatformContext _db;

        public AllRepository(CiplatformContext db)
        {
            _db= db;
            missionRepository=new MissionRepository(_db);
            volunteerRepository = new VolunteerRepository(_db); 
            storyRepository = new StoryRepository(_db);
        }
        public IMissionRepository missionRepository { get; set; }
        public IVolunteerRepository volunteerRepository { get; set ; }
        public IStoryRepository storyRepository { get; set ; }

     

        public bool AddFavouriteMission(long MissionId, long UserId)
        {

            FavoriteMission favoriteMission = new FavoriteMission();
            favoriteMission.UserId = UserId;
            favoriteMission.MissionId = MissionId;

            var favmission = _db.FavoriteMissions.FirstOrDefault(s => s.UserId == UserId && s.MissionId == MissionId);
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

        public bool ApplyMission(long MissionId, long UserId)
        {
            MissionApplication missionApplication = new MissionApplication();
            missionApplication.MissionId = MissionId;
            missionApplication.UserId = UserId;

            var applymission = _db.MissionApplications.FirstOrDefault(s => s.MissionId == MissionId && s.UserId == UserId);

            if (applymission != null)
            {
                _db.MissionApplications.Add(missionApplication);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
