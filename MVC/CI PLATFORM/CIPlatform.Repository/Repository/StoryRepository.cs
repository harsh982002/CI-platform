using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public bool AddStory(long user_id, long id, long mission_id, string title, string mystory, List<string> media, string type)
        {
            CIPlatform.Entitites.Models.Story story = new CIPlatform.Entitites.Models.Story();
            if (type == "PUBLISHED")
            {
                //if story is save in draft

                if (id != 0)
                {
                    CIPlatform.Entitites.Models.Story edit_story = _db.Stories.FirstOrDefault(c => c.StoryId.Equals(id));
                    edit_story.Title = title;
                   
                    edit_story.Description = mystory;
                    edit_story.Status = "PUBLISHED";
                    List<StoryMedium> storymedias = (from m in medias
                                                    where m.StoryId == id
                                                    select m).ToList();
                    _db.StoryMedia.RemoveRange(storymedias);
                    foreach (var item in media)
                    {
                        _db.StoryMedia.Add(new StoryMedium
                        {
                            StoryId = id,
                            Type = "images",
                            Path = item
                        });
                    }
                    _db.SaveChanges();
                }

                //direct published at first time

                else
                {
                    story.UserId = user_id;
                    story.MissionId = mission_id;
                    story.Title = title;
                    story.Description = mystory;
                   
                    story.Status = "PUBLISHED";
                    _db.Stories.Add(story);
                    _db.SaveChanges();
                    long story_id = story.StoryId;
                    foreach (var item in media)
                    {
                        _db.StoryMedia.Add(new StoryMedium
                        {
                            StoryId = story_id,
                            Type = "images",
                            Path = item
                        });
                    }
                }
            }

            //if user save story 
            else
            {
                story.UserId = user_id;
                story.MissionId = mission_id;
                story.Title = title;
                story.Description = mystory;
              
                _db.Stories.Add(story);
                _db.SaveChanges();
                long story_id = story.StoryId;
                foreach (var item in media)
                {
                    _db.StoryMedia.Add(new StoryMedium
                    {
                        StoryId = story_id,
                        Type = "images",
                        Path = item
                    });
                }

            }
            _db.SaveChanges();
            return true;

        }

        public CIPlatform.Entitites.ViewModel.Mission GetStories(long user_id)
        {
            stories = (from s in stories
                       where s.Status == "PUBLISHED" || s.UserId == user_id
                       orderby s.Status ascending
                       select s).ToList();
            return new CIPlatform.Entitites.ViewModel.Mission { Stories = stories.Take(9).ToList(), total_missions = stories.Count };
        }

        public Entitites.ViewModel.Mission GetFileredStories(int page_index, long user_id)
        {
            //get stories as per page
            stories = (from s in stories
                       where s.Status == "PUBLISHED" || s.UserId == user_id
                       orderby s.Status ascending
                       select s).ToList();
            return new CIPlatform.Entitites.ViewModel.Mission { Stories = stories.Skip(9 * page_index).Take(9).ToList() };
        }

        public List<Mission> mission_of_user(long user_id)
        {
            List<CIPlatform.Entitites.Models.Mission> User_Applied_Missions = (from m in missionApplications
                                                     where m.UserId == user_id
                                                     select m.Mission).ToList();
            return User_Applied_Missions;
        }

        public Entitites.ViewModel.StoryViewModel GetStory(long user_id, long Storyid)
        {
            List<User> users = _db.Users.ToList();
            var story = _db.Stories.FirstOrDefault(c => c.StoryId == Storyid);
            if (story is not null)
            {
                List<User> already_recommended = new List<User>();
               
               
                return new Entitites.ViewModel.StoryViewModel { story = story, Users = users };
            }
            else
            {
                return null;
            }
        }
    }
}
