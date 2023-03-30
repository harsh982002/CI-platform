using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
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
        List<Comment> comments = new List<Comment>();
        List<User> users = new List<User>();
        List<MissionApplication> missionApplications = new List<MissionApplication>();
        List<FavoriteMission> favoriteMissions = new List<FavoriteMission>();
        List<MissionRating> ratings = new List<MissionRating>();
        List<MissionInvite> already_recommended_users = new List<MissionInvite>();
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
            comments = _db.Comments.ToList();
            users = _db.Users.ToList();
            missionApplications = _db.MissionApplications.ToList();
            favoriteMissions = _db.FavoriteMissions.ToList();
            ratings = _db.MissionRatings.ToList();
            already_recommended_users = _db.MissionInvites.ToList();
        }

        public bool add_to_favourite(long user_id, long mission_id)
        {
            if (user_id != 0 && mission_id != 0)
            {
                var favouritemission = (from fm in favoriteMissions
                                        where fm.UserId.Equals(user_id) && fm.MissionId.Equals(mission_id)
                                        select fm).ToList();
                if (favouritemission.Count == 0)
                {
                    _db.FavoriteMissions.Add(new FavoriteMission
                    {
                        UserId = user_id,
                        MissionId = mission_id
                    });
                    Save();
                    return true;
                }
                else
                {
                    _db.Remove(favouritemission.ElementAt(0));
                    Save();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        public void AddComment(string userComment, long MissionId, long userId)
        {
            Comment comment = new Comment();
            comment.UserId = userId;
            comment.Comment1 = userComment;
            comment.MissionId = MissionId;
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        public Entitites.ViewModel.Mission GetAllMission()
        {
            
            int total_missions = missions.Count;
            missions = missions.Take(6).ToList();
            var Missions = new CIPlatform.Entitites.ViewModel.Mission { Missions = missions, Country = countries, themes = theme, skills = skills, total_missions = total_missions,  };
            return Missions;
        }

        public Entitites.ViewModel.Mission GetFilteredMissions(List<string> Countries, List<string> Cities, List<string> Themes, List<string> Skills, string sort_by, int page_index, long user_id)
        {

            CIPlatform.Entitites.ViewModel.Mission Missions = new Entitites.ViewModel.Mission();
            List<City> city = new List<City>();
            List<CIPlatform.Entitites.Models.Mission> mission = new List<CIPlatform.Entitites.Models.Mission>();

            //get missions as per page
            if (page_index != 0)
            {
                missions = missions.Skip(6 * page_index).Take(6).ToList();
            }
            else
            {
                missions = missions.Take(6).ToList();
            }

            //get cities as per country
            if (Countries.Count > 0)
            {
                city = (from c in cities
                        where Countries.Contains(c.Country.Name)
                        select c).ToList();
            }
            else
            {
                city = cities;
            }
            //filter as per city,theme and skill, if country selected it doesn't matter because filter work as per city first
            if (Cities.Count > 0)
            {

                mission = (from m in missions
                           where Cities.Contains(m.City.Name) || Themes.Contains(m.Theme.Title)
                           select m).ToList();
                var skill_missions = (from s in missionskills
                                      where Skills.Contains(s.Skill.SkillName) && missions.Contains(s.Mission)
                                      select s.Mission).ToList();
                foreach (var skill_mission in skill_missions)
                {
                    if (!mission.Contains(skill_mission))
                    {
                        mission.Add(skill_mission);
                    }
                }
            }

            //filter as per country,theme and skills if city not selected
            else if (Countries.Count > 0 || Themes.Count > 0 || Skills.Count > 0)
            {
                mission = (from m in missions
                           where Countries.Contains(m.Country.Name) || Cities.Contains(m.City.Name) || Themes.Contains(m.Theme.Title)
                           select m).ToList();
                var skill_missions = (from s in missionskills
                                      where Skills.Contains(s.Skill.SkillName) && missions.Contains(s.Mission)
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

            //sort all mission as per user need
            if (sort_by == "newest")
            {
                Missions = new Entitites.ViewModel.Mission
                {
                    Missions = (from m in mission
                                orderby m.CreatedAt descending
                                select m).ToList(),
                    Country = countries,
                    Cities = city
                };
            }
            else if (sort_by == "lowest available seats")
            {
                Missions = new Entitites.ViewModel.Mission
                {
                    Missions = (from m in mission
                                orderby m.AvbSeat ascending
                                select m).ToList(),
                    Country = countries,
                    Cities = city
                };
            }
            else if (sort_by == "highest available seats")
            {
                Missions = new Entitites.ViewModel.Mission
                {
                    Missions = (from m in mission
                                orderby m.AvbSeat descending
                                select m).ToList(),
                    Country = countries,
                    Cities = city
                };
            }
            else if (sort_by == "registration deadline")
            {
                Missions = new Entitites.ViewModel.Mission
                {
                    Missions = (from m in mission
                                orderby m.Deadline ascending
                                select m).ToList(),
                    Country = countries,
                    Cities = city
                };
            }
            else if (sort_by == "my favourites")
            {
                Missions = new Entitites.ViewModel.Mission
                {
                    Missions = (from m in favoriteMissions
                                where m.UserId == user_id && mission.Contains(m.Mission)
                                select m.Mission).ToList(),
                    Country = countries,
                    Cities = city
                };
            }

            //if no filter apply
            else
            {

                Missions = new Entitites.ViewModel.Mission
                {
                    Missions = mission,
                    Country = countries,
                    Cities = city
                };
            }
            return Missions;
        }

        public Entitites.ViewModel.Mission GetSearchMissions(string key, int page_index)
        {
            if (page_index != 0)
            {
                missions = missions.Skip(6 * page_index).Take(6).ToList();
            }
            else
            {
                missions = missions.Take(6).ToList();
            }
            var mission = (from m in missions
                           where m.Title.ToLower().Contains(key) || m.Description.ToLower().Contains(key)
                           select m).ToList();
            var Missions = new Entitites.ViewModel.Mission
            {
                Missions = mission,
            };
            return Missions;
        }

        CIPlatform.Entitites.ViewModel.VolunteerViewModel IMissionRepository.Mission(long id, long user_id)
        {
            int rate = 0;
            int avg_rating = 0;
            var fav = _db.FavoriteMissions.Where(x => x.MissionId == id && x.UserId == user_id).Count();
            var application = _db.MissionApplications.Where(x => x.MissionId == id && x.UserId == user_id).Count();
            /*var recentvol = _db.MissionApplications.Where(x => x.MissionId == id && x.UserId == user_id).ToList();*/
            var user = _db.Users.ToList();
            if(_db.MissionRatings.FirstOrDefault(x => x.MissionId == id && x.UserId == user_id) is not null)
            {
                rate = _db.MissionRatings.FirstOrDefault(x => x.MissionId == id && x.UserId == user_id).Rating;
            }
            
            Entitites.Models.Mission? mission = _db.Missions.Find(id);
            if (mission is not null)
            {

                double avg_ratings = 0;
                bool applied_or_not = false;
                int rating_count = 0;
                int rating = 0;
                

                List<User> users = new List<User>();

                List<MissionApplication> applied = (from ma in missionApplications
                                                    where ma.MissionId.Equals(mission?.MissionId) && ma.UserId.Equals(user_id)
                                                    select ma).ToList();
                if (applied.Count > 0)
                {
                    applied_or_not = true;
                }

                //get rating if user already rated
                var Rating = (from r in ratings
                              where r.UserId.Equals(user_id) && r.MissionId.Equals(id)
                              select r).ToList();
                if (Rating.Count > 0)
                {
                    rating = Rating.ElementAt(0).Rating;
                }

                

                //get mission

                //find avg of mission rating
                if (mission.MissionRatings.Count > 0)
                {
                    avg_ratings = (from m in mission.MissionRatings
                                   select m.Rating).Average();
                    rating_count = (from m in mission.MissionRatings
                                    select m).ToList().Count;
                }


                //get related missions
                List<Entitites.Models.Mission> related_mission = (from m in missions
                                                                  where m.City.Name.Equals(mission?.City.Name) && !m.MissionId.Equals(mission.MissionId)
                                                                  select m).Take(3).ToList();




                //get related mission if no mission available in city
                if (related_mission.Count == 0)
                {
                    related_mission = (from m in missions
                                       where m.Country.Name.Equals(mission?.Country.Name) && !m.MissionId.Equals(mission.MissionId)
                                       select m).Take(3).ToList();
                    if (related_mission.Count == 0)
                    {
                        related_mission = (from m in missions
                                           where m.Theme.Title.Equals(mission?.Theme.Title) && !m.MissionId.Equals(mission.MissionId)
                                           select m).Take(3).ToList();
                    }
                }

                return new CIPlatform.Entitites.ViewModel.VolunteerViewModel { mission = mission, related_mission = related_mission, users = user,Favorite_mission= fav, applyuser= application, Rating= rate, Avg_ratings = avg_ratings, Rating_count = rating_count, };
            }
            else
            {
                return new CIPlatform.Entitites.ViewModel.VolunteerViewModel { mission = mission, users = user, Favorite_mission = fav, applyuser = application , Rating = rate };
            }

        }



        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool ApplyMission(long mission_id, long user_id)
        {
            MissionApplication missionapplication = new MissionApplication();
            missionapplication.UserId = user_id;
            missionapplication.MissionId = mission_id;
            missionapplication.AppliedAt = DateTime.Now;

            var applymission = _db.MissionApplications.FirstOrDefault(s => s.MissionId == mission_id && s.UserId == user_id);

            if (applymission == null)
            {
                _db.MissionApplications.Add(missionapplication);
                _db.SaveChanges();
                return true;
            }
            return false;
        }


            public bool sendMail(string[] emailList, long mission_id, long user_id)
        {

            User fromUser = _db.Users.FirstOrDefault(u => u.UserId == user_id);
            foreach (var item in emailList)
            {
                User toUser = _db.Users.FirstOrDefault(u => u.Email == item);
                MissionInvite missionInvite = new MissionInvite();
                missionInvite.FromUserId = user_id;
                missionInvite.ToUserId = toUser.UserId;
                missionInvite.MissionId = mission_id;
                missionInvite.CreatedAt = DateTime.Now;
                _db.MissionInvites.Add(missionInvite);

            }
            _db.SaveChanges();


            var mailBody = "<h1>" + fromUser.FirstName + " Recommended Mission</h1><br><h2><a href='" + "https://localhost:44335/Home/Volunteermission/" + mission_id + "'>Go to Mission</a></h2>";
           
            foreach (var item in emailList)
            {

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("parthv480@gmail.com"));
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

        public bool addRatings(int rating, long mission_id, long userId)
        {
            var missionRating = new MissionRating();
            var alradyRate = _db.MissionRatings.Where(x => x.MissionId == mission_id && x.UserId == userId).Count();

            if (alradyRate == 0)
            {
                missionRating.MissionId = mission_id;
                missionRating.Rating = rating;
                missionRating.UserId = userId;
                missionRating.CreatedAt= DateTime.Now;  
                _db.MissionRatings.Add(missionRating);
            }
            else
            {
                missionRating = (MissionRating)_db.MissionRatings.FirstOrDefault(x => x.MissionId == mission_id && x.UserId == userId);
                missionRating.Rating = rating;
                missionRating.UpdatedAt = DateTime.Now;
                _db.MissionRatings.Update(missionRating);
            }
            _db.SaveChanges();

            return true;
        }

    }
}
