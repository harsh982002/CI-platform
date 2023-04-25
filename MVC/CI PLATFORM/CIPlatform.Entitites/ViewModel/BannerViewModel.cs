using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class BannerViewModel
    {
       
        public long BannerId { get; set; }

        public string? Image { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "Field can't be empty!")]
        public int? SortOrder { get; set; }

        public IFormFile? BannerImage { get; set; }
        public List<BannerViewModel> Bans { get; set; }
    }
}
