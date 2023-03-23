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
        public VolunteerViewModel Missiondetails(long Id)
        {
            
            var missions = _db.Missions.FirstOrDefault(m => m.MissionId == Id);
            var cities = _db.Cities.ToList();
            var countries = _db.Countries.ToList();
            
            var themes = _db.MissionThemes.ToList();
            var user = _db.Users.ToList();
            var skills = _db.Skills.ToList();
            var missionskills = _db.MissionSkills.ToList();
            var mission_documents = _db.MissionDocuments.ToList();
            var image = _db.MissionMedia.FirstOrDefault(x => x.MissionId == Id).MediaPath;
            var comment = _db.Comments.ToList();
            var recentvol = _db.MissionApplications.ToList();
            var rating = _db.MissionRatings.ToList();
            
            VolunteerViewModel data = new VolunteerViewModel
            {
                Missions = missions, Cities = cities, Country = countries, themes = themes, skills = skills, Image = image , comments = comment, MissionApplications = recentvol, users = user , missionRating = rating,
            };
            return data;
        }

        
        public List<MissionRating> getMissionRatings(long MissionId)
        {
            List<MissionRating> rating = _db.MissionRatings.Where(a => a.MissionId == MissionId).ToList();
            return rating;
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


        /* public bool ApplyMission(long MissionId, long UserId)
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
         }*/

        public bool ApplyMission(long MissionId, long UserId)
        {
            MissionApplication missionapplication = new MissionApplication();
            missionapplication.UserId = UserId;
            missionapplication.MissionId = MissionId;
            missionapplication.AppliedAt = DateTime.Now;

            var applymission = _db.MissionApplications.FirstOrDefault(s => s.MissionId == MissionId && s.UserId == UserId);

            if (applymission == null)
            {
                _db.MissionApplications.Add(missionapplication);
                _db.SaveChanges();
                return true;
            }   
            return false;
            
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
                if (favmission.DeletedAt == null)
                {
                    favmission.DeletedAt = DateTime.Now;
                    // if error occurs then remove below line and check whether it works or not. also check the getdate() as default in createdat
                    //also check for createdat and appliedat in mission application table why getdate() not working...
                    //_CIPlatformDbContext.FavoriteMissions.Update(favourite);
                    _db.SaveChanges();
                    return false;
                }
                else
                {
                    if (favmission.UpdatedAt == null)
                    {
                        favmission.UpdatedAt = DateTime.Now;
                        //_CIPlatformDbContext.FavoriteMissions.Update(favourite);
                        _db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        if (favmission.DeletedAt < favmission.UpdatedAt)
                        {
                            favmission.DeletedAt = DateTime.Now;
                            //_CIPlatformDbContext.FavoriteMissions.Update(favourite);
                            _db.SaveChanges();
                            return false;
                        }
                        favmission.UpdatedAt = DateTime.Now;
                        //_CIPlatformDbContext.FavoriteMissions.Update(favourite);
                        _db.SaveChanges();
                        return true;
                    }
                }
            }
        }

       /* public List<CommentViewModel> GetComments(long MissionId)
        {
            List<Comment> comments = _db.Comments.Where(a => a.MissionId == MissionId && a.approvalstatus == "PUBLISHED").ToList();
            List<CommentViewModel> commentViewModels = new List<CommentViewModel>();
            foreach (Comment comment in comments)
            {
                CommentViewModel commentViewModel = new CommentViewModel();
                User user = _db.Users.FirstOrDefault(a => a.UserId == comment.UserId);
                commentViewModel.Avatar = user.Avatar;
                commentViewModel.Comment = comment.Comment1;
                commentViewModel.UserName = user.FirstName + " " + user.LastName;
                commentViewModel.WeekDay = comment.CreatedAt.DayOfWeek.ToString();
                commentViewModel.Time = comment.CreatedAt.ToString("h:mm tt");
                commentViewModel.Month = comment.CreatedAt.ToString("MMM");
                commentViewModel.Day = comment.CreatedAt.Day.ToString();
                commentViewModel.Year = comment.CreatedAt.Year.ToString();

                commentViewModels.Add(commentViewModel);
            }
            return commentViewModels;
        }*/

        public void AddComment(string userComment, long MissionId, long userId)
        {
            Comment comment = new Comment();
            comment.UserId = userId;
            comment.Comment1 = userComment;
            comment.MissionId = MissionId;
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }
        private void Save()
        {
            throw new NotImplementedException();
        }

        public bool Ratemission(long MissionId, long UserId, int rating)
        {

            var Rating = (from r in ratings
                          where r.UserId.Equals(UserId) && r.MissionId.Equals(MissionId)
                          select r).ToList();
            if (Rating.Count == 0)
            {
                _db.MissionRatings.Add(new MissionRating
                {
                    UserId = UserId,
                    MissionId = MissionId,
                    Rating = rating
                });
                Save();
                return true;
            }
            else
            {
                Rating.ElementAt(0).Rating = rating;
                Save();
                return true;
            }

        }

        public bool sendMail(string[] emailList, long MissionId, long userId)
        {
            User fromUser = _db.Users.FirstOrDefault(u => u.UserId == userId);
            foreach (var item in emailList)
            {
                User toUser = _db.Users.FirstOrDefault(u => u.Email == item);
                MissionInvite missionInvite = new MissionInvite();
                missionInvite.FromUserId = userId;
                missionInvite.ToUserId = toUser.UserId;
                missionInvite.MissionId = MissionId;
                missionInvite.CreatedAt = DateTime.Now; 
                _db.MissionInvites.Add(missionInvite);

            }
                _db.SaveChanges();
          

            var mailBody = "<h1>" + fromUser.FirstName + "Recommended Mission</h1><br><h2><a href='" + "https://localhost:44367/Home/Volunteermission?id=" + MissionId + "'>Go to Mission</a></h2>";
            //var mailBody = "<h1>" + HttpContext.Session.GetString("UserName") + " Suggest Mission : " + mission.Title + " to You</h1><br><h2><a href='https://localhost:7227/Mission/MisionDetail?id= " + model.MissionId + "'>Go Website</a></h2>";
            //create email message
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


    }
}
