﻿using CIPlatform.Entitites.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class ProfileViewModel
    {
        public List<Country>? countries { get; set; }   

        public List<City>? cities { get; set;}

        public List<Skill>? skill { get; set; }

        public List<UserSkill>? userSkills { get; set; }

        public IFormFile? profile { get; set; }
        public string? selected_skills { get; set; }


        [Required(ErrorMessage = "Please enter your First name.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last name.")]
        public string? LastName { get; set; }

        public string? Avatar { get; set; }

        public string? WhyIVolunteer { get; set; }

        public string? EmployeeId { get; set; }

        public string? Department { get; set; }
        
        [Required]
        public long? CityId { get; set; } = 0;
        [Required]
        public long? CountryId { get; set; } = 0;
        [Required]
        [MinLength(20, ErrorMessage = "Bio Is Too Short")]
        public string? ProfileText { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? Title { get; set; }

        public string? Availability { get; set; }

        [Required]
        [MaxLength(255)]
        public string Subject { get; set; } = null!;

        [Required]
        [MaxLength(60000)]
        public string Message { get; set; } = null!;

    }
}
