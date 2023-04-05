using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class EditViewModel
    {

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
    }
}
