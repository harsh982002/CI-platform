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
            var comment = _db.Comments.ToList();
            VolunteerViewModel data = new VolunteerViewModel
            {
                Missions = missions, Cities = cities, Country = countries, themes = themes, skills = skills, Image = image , comments = comment
            };
            return data;
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

        public void ApplyMission(long MissionId, long UserId)
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
               
            }   
            else
            {
                missionapplication.ApprovalStatus = "PENDING";
                missionapplication.AppliedAt= DateTime.Now;
            }
            _db.SaveChanges();
            
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


    }
}
