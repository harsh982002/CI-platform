using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IVolunteerRepository : IRepository<CIPlatform.Entitites.Models.Mission>
    {
        void save();

        
        VolunteerViewModel Missiondetails(int Id);
        List<MissionRating> getMissionRatings(int Id);

        /* bool ApplyMission(long UserId, long MissionId);*/

        bool ApplyMission(long MissionId, long UserId);

        bool sendMail(string[] email, long MissionId, long UserId);

        void AddComment(string comment, long MissonId, long userId);
/*        List<CommentViewModel> GetComments(long missionId);*/
    }
}
