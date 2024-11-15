﻿using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Repository.Interface;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CIPlatform.Repository.Repository
{
    public class StoryRepository : Repository<CIPlatform.Entitites.Models.Story>, IStoryRepository
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
           

        }
        public bool AddStory(long user_id, long id, long mission_id, string title, string mystory, List<string> media, string type)
        {
            CIPlatform.Entitites.Models.Story story = new CIPlatform.Entitites.Models.Story();
            if (type == "PUBLISHED")
            {
                //if story is save in draft

                if (id != 0)
                {
                    CIPlatform.Entitites.Models.Story? edit_story = _db.Stories.FirstOrDefault(c => c.StoryId.Equals(id));
                    edit_story.Title = title;
                    edit_story.StatusUserwant = type;
                    edit_story.Description = mystory;
                    edit_story.Status = "PENDING";
                    List<StoryMedium> storymedias = (from m in medias
                                                     where m.StoryId == id
                                                     select m).ToList();
                    _db.StoryMedia.RemoveRange(storymedias);
                    if (media.ElementAt(0).Length > 300)
                    {
                        foreach (var item in media)
                        {
                            _db.StoryMedia.Add(new StoryMedium
                            {
                                StoryId = id,
                                Type = "images",
                                Path = item
                            });
                        }
                    }
                    else
                    {

                        _db.StoryMedia.Add(new StoryMedium
                        {
                            StoryId = id,
                            Type = "video",
                            Path = media.ElementAt(0)
                        });
                        media.RemoveAt(0);
                        foreach (var item in media)
                        {
                            _db.StoryMedia.Add(new StoryMedium
                            {
                                StoryId = id,
                                Type = "images",
                                Path = item
                            });
                        }
                    }
                    _db.SaveChanges();
                }

                //direct published at first time

                else
                {
                    story.UserId = user_id;
                    story.MissionId = mission_id;
                    story.Title = title;
                    story.StatusUserwant = type;
                    story.Description = mystory;
                    story.Status = "PENDING";
                    _db.Stories.Add(story);
                    _db.SaveChanges();
                    long story_id = story.StoryId;
                    if (media.ElementAt(0).Length > 300)
                    {
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
                    else
                    {

                        _db.StoryMedia.Add(new StoryMedium
                        {
                            StoryId = story_id,
                            Type = "video",
                            Path = media.ElementAt(0)
                        });
                        media.RemoveAt(0);
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
            }

            //if user save story 
            else
            {
                story.UserId = user_id;
                story.MissionId = mission_id;
                story.Title = title;
                story.StatusUserwant = "DRAFT";
                story.Description = mystory;

                _db.Stories.Add(story);
                _db.SaveChanges();
                long story_id = story.StoryId;
                if (media.ElementAt(0).Length > 300)
                {
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
                else
                {

                    _db.StoryMedia.Add(new StoryMedium
                    {
                        StoryId = story_id,
                        Type = "video",
                        Path = media.ElementAt(0)
                    });
                    media.RemoveAt(0);
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
            _db.SaveChanges();
            return true;

        }

        public CIPlatform.Entitites.ViewModel.Mission GetStories(long user_id)
        {
            stories = (from s in stories
                       where s.Status == "PUBLISHED" || (s.UserId == user_id && s.Status == "DRAFT")
                       orderby s.Status ascending
                       select s).ToList();
            return new CIPlatform.Entitites.ViewModel.Mission { Stories = stories.Take(1).ToList(), total_missions = stories.Count };
        }

        public Entitites.ViewModel.Mission GetFileredStories(int page_index, long user_id)
        {
            //get stories as per page
            stories = (from s in stories
                       where s.Status == "PUBLISHED" || s.UserId == user_id
                       orderby s.Status ascending
                       select s).ToList();
            return new CIPlatform.Entitites.ViewModel.Mission { Stories = stories.Skip(1 * page_index).Take(1).ToList() };
        }

        public List<Mission> mission_of_user(long user_id)
        {
            CIPlatform.Entitites.Models.MissionApplication mission = new CIPlatform.Entitites.Models.MissionApplication();
            mission.ApprovalStatus = "APPROVE";
            var story = _db.Stories.Where(s=>s.UserId == user_id && s.StatusUserwant =="DRAFT").FirstOrDefault();
            List<CIPlatform.Entitites.Models.Mission> User_Applied_Missions = (from m in missionApplications
                                                                               where m.UserId == user_id && !m.Mission.Stories.Contains(story)
                                                                               select m.Mission).ToList();
            return User_Applied_Missions;
        }

        public Entitites.ViewModel.StoryViewModel GetStory(long user_id, long Storyid)
        {
            var viewer = _db.StoryViews.Where(x => x.StoryId.Equals(Storyid)).ToList().Count;
            List<User> users = _db.Users.ToList();
            
            var story = _db.Stories.FirstOrDefault(c => c.StoryId == Storyid);
            var alreaduInvite = _db.StoryInvites.Where(x => x.FromUserId == user_id && x.StoryId == story.StoryId).Include(x => x.ToUser).ToList();
            foreach (var i in alreaduInvite)
            {
                users = users.Where(x => x.UserId != i.ToUserId).ToList();
            }
            if (story is not null)
            {
                List<User> already_recommended = new List<User>();
                return new Entitites.ViewModel.StoryViewModel { story = story, Users = users, StoryViews = viewer ,alreadyrecommend = alreaduInvite};
            }
            else
            {
                return null;
            }
        }

        public bool Recommend(string[] emailList, long story_id, long user_id)
        {

            User? fromUser = _db.Users.FirstOrDefault(u => u.UserId == user_id);
            foreach (var item in emailList)
            {
                User? toUser = _db.Users.FirstOrDefault(u => u.Email == item);
                StoryInvite? storyInvite = new StoryInvite();
                storyInvite.FromUserId = user_id;
                storyInvite.ToUserId = toUser.UserId;
                storyInvite.StoryId = story_id;
                storyInvite.CreatedAt = DateTime.Now;
                _db.StoryInvites.Add(storyInvite);

            }
            _db.SaveChanges();


            var mailBody = "<h1>" + fromUser.FirstName + " Recommended you a story</h1><br><h2><a href='" + "https://localhost:44335/Story/StoryDetails/" + story_id + "'>Go to Story</a></h2>";

            foreach (var item in emailList)
            {

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("harshrathod982002@gmail.com"));
                email.To.Add(MailboxAddress.Parse(item));
                email.Subject = "Co-Worker Suggestion";
                email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("harshrathod982002@gmail.com", "ejvjkaneyltiqawq");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            return true;
        }

        public void Views(long user_id, long Storyid)
        {
            var viewer = _db.StoryViews.FirstOrDefault(x => x.UserId.Equals(user_id) && x.StoryId.Equals(Storyid));

            if (viewer is null)
            {
                _db.StoryViews.Add(new StoryView { StoryId = Storyid, UserId = user_id });
                _db.SaveChanges();
            }
        }
    }
}
