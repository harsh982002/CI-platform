using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entitites.ViewModel
{
    public class UserViewModel
    {
        public long? user_id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? EmpId { get; set; }

        public string? Department { get; set; }

        public string? status { get; set; }
    }
}
