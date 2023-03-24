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
    public class StoryRepository: Repository<CIPlatform.Entitites.Models.Story>, IStoryRepository
    {
        private readonly CiplatformContext _db;
        List<MissionApplication> missionApplications = new List<MissionApplication>();
        List<CIPlatform.Entitites.Models.StoryMedium> medias = new List<StoryMedium>();
        List<Entitites.Models.Story> stories = new List<Story>();
        List<User> users = new List<User>();
        List<StoryInvite> already_recommended_users = new List<StoryInvite>();
       
        public StoryRepository(CiplatformContext db) : base(db)
        {
            _db = db;
            getdetails();
        }

        private void getdetails()
        {
            missionApplications = _db.MissionApplications.ToList();
            medias = _db.StoryMedia.ToList();
            stories = _db.Stories.ToList();
            users = _db.Users.ToList();
            already_recommended_users = _db.StoryInvites.ToList();
            
        }

        public CIPlatform.Entitites.ViewModel.Mission GetStories(long user_id)
        {
            stories = (from s in stories
                       where s.Status == "PUBLISHED" || s.UserId == user_id
                       orderby s.Status ascending
                       select s).ToList();
            return new CIPlatform.Entitites.ViewModel.Mission { Stories = stories.Take(9).ToList(), total_missions = stories.Count };
        }

        public List<Mission> mission_of_user(long user_id)
        {
            List<CIPlatform.Entitites.Models.Mission> User_Applied_Missions = (from m in missionApplications
                                                     where m.UserId == user_id
                                                     select m.Mission).ToList();
            return User_Applied_Missions;
        }
    }
}
