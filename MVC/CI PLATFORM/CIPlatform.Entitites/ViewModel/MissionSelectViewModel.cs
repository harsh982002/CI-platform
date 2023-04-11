using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class MissionSelectViewModel
    {
        public long? mission_id { get; set; }

        public string? Title { get; set; }

        public string? missiontype { get; set; }

        public string? goalobject { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
