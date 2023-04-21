using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class MissionViewModel
    {
        public List<MissionSelectViewModel>? Missions { get; set; }

        public MissionSelectViewModel? mission_detail { get; set; }

        public List<Country>? countries { get; set; }

        public List<City>? citys { get; set; }

        public List<SkillViewModel>? skills { get; set; }
        public List<MissionThemeViewModel>? theme { get; set; }
    }
}
