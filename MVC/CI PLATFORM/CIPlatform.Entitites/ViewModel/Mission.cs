using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class Mission
    {
        public int? total_missions { get; set; }
        public List<Story>? Stories { get; set; }
        public List<CIPlatform.Entitites.Models.Mission>? Missions { get; set; }
        public List<Country>? Country { get; set; }
        public List<City>? Cities { get; set; }
        public List<MissionTheme>? themes { get; set; }
        public List<Skill>? skills { get; set; }

        public List<User>? users { get; set; }

        public string? applyuser { get; set; }
       
        public List<User>? Recent_volunteers { get; set; }
        public List<User>? All_volunteers { get; set; }
        public int? Total_volunteers { get; set; }
        public int? Favorite_mission { get; set; }
        public int? Rating { get; set; }

        public int? userapply { get; set; }

        public double? Avg_ratings { get; set; }
    }
}
