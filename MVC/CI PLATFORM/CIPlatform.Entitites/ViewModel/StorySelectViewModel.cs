using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class StorySelectViewModel
    {
        public long? StoryId { get; set; }

        public string? StoryName { get; set; }

        public string? UserName { get; set; }

        public long? MissionId { get; set; }

        public string? MissionName { get; set; }
    }
}
