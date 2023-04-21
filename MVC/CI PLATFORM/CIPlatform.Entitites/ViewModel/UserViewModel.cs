using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class UserViewModel
    {
        public long? user_id { get; set; }

        [Required(ErrorMessage = "Please enter your First name.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last name.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required]
        public string? EmpId { get; set; }

        public string? Department { get; set; }

        [MinLength(10)]
        [Required(ErrorMessage = "Please enter your Phonenumber.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        public string? Role { get; set; }

        public string? status { get; set; }

        public long? CityId { get; set; }

        public long? CountryId { get; set; }

        public string? ProfileText { get; set; }

        public string? Avatar { get; set; }

        public List<UserViewModel>? UserViewModels { get; set; }
    }
}
