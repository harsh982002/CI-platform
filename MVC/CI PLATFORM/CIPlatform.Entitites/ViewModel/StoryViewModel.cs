using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class StoryViewModel
    {
        public CIPlatform.Entitites.Models.Story? story{ get; set; }

        public List<User> Users { get; set; }


    }
}
