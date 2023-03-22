using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class VolunteerViewModel
    {
       

        public CIPlatform.Entitites.Models.Mission? Missions { get; set; }
        
        public int? UserId { get; set; }
        
        public string? Image { get; set; }
        public List<Country>? Country { get; set; }
        public List<Comment> comments { get; set; }

        public List<User> users { get; set; }
        
        public List<MissionRating> missionRating { get; set; }  

        public List<MissionApplication> MissionApplications { get; set; }
        public List<City>? Cities { get; set; }
        public List<MissionTheme>? themes { get; set; }
        public List<Skill>? skills { get; set; }
        public string? Mission_city { get; set; }
        public string? Mission_theme { get; set; }
        public int? TotalMission { get; set; }

        public int Rating { get; set; }
        public int RatingUser { get; set; }

        public List<Mission> RelatedMissions { get; set; }
 

    }
}
