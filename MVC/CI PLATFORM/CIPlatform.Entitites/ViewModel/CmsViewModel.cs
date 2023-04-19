using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class CmsViewModel
    {
        public long? CmsPageId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Slug { get; set; }

        public string? Status { get; set; }

        public List<CIPlatform.Entitites.Models.CmsPage>? cmsPages { get; set; }
    }
}
