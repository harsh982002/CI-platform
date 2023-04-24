using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class BannerViewModel
    {
        public long BannerId { get; set; }

        public string? Image { get; set; }

        public string? Text { get; set; }

        public int? SortOrder { get; set; }

        public List<BannerViewModel> Bans { get; set; }
    }
}
