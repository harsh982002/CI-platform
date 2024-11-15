﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class SkillViewModel
    {
        public int? SkillId { get; set; }
        [Required(ErrorMessage = "Skills can't be empty!")]
        public string? SkillName { get; set; }
        public byte? Status { get; set; }

        public List<SkillViewModel>? SkillList { get; set; }
    }
}
