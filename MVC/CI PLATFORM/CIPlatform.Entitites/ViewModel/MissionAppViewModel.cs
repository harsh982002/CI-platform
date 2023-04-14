using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class MissionAppViewModel
    {
        public long? mission_id { get; set; }

        public long? user_id { get; set; }

        public string? Title { get; set; }

        public string? name { get; set; }

        public DateTime? AppliedAt { get; set; }
    }
}
