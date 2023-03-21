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
        public string UserName { get; set; }
        public string Time { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
        public string Avatar { get; set; }
        public string WeekDay { get; set; }
        public string Comment { get; set; }
    }
}
