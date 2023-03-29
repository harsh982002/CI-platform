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


        public CIPlatform.Entitites.Models.Mission? mission { get; set; }
        public List<CIPlatform.Entitites.Models.Mission>? related_mission { get; set; }
        public List<User>? Recent_volunteers { get; set; }
        public List<Comment> comments { get; set; }

        public List<MissionSkill> mission_skill { get; set; }

        public int? applyuser { get; set; }
        public List<User>? All_volunteers { get; set; }
        public int? Total_volunteers { get; set; }
        public int? Favorite_mission { get; set; }
        public int? Rating { get; set; }
        public List<User> users { get; set; }
        public double? Avg_ratings { get; set; }
        public int? Rating_count { get; set; }
        public bool Applied_or_not { get; set; }



    }
}
