using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class CommentViewModel
    {
        public User? user { get; set; }

        public Comment? Comment { get; set; }
    }
}
