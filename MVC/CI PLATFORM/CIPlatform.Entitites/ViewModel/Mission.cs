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
        public int total_missions;

        public CIPlatform.Entitites.Models.Mission? Missions { get; set; }

        
        
        public MissionMedium? image { get; set; }
        public List<Country>? Country { get; set; }
        public List<City>? Cities { get; set; }
        public List<MissionTheme>? themes { get; set; }
        public List<Skill>? skills { get; set; }
        public string? Mission_city { get; set; }
        public string? Mission_theme { get; set; }
        public int? TotalMission { get; set; }
        public List<Story> Stories { get; set; }
    }
}
