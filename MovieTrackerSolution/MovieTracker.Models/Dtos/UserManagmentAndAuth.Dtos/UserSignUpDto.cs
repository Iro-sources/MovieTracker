using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos
{
    public class UserSignUpDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Dictionary<string, bool> GenreConfig { get; set; }
    }
}
