﻿using CIPlatform.Entitites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class CommentViewModel
    {
        public Comment? comment { get; set; }
        public User? user { get; set; }
    }
}
