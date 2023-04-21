using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class MissionThemeViewModel
    {
        public long? theme_id { get; set; }
        [Required(ErrorMessage = "Theme can't be empty!")]
        public string? theme_name { get; set; }

        public byte? status { get; set; }

        public List<MissionThemeViewModel>? MissionThemes { get; set; }
    }
}
