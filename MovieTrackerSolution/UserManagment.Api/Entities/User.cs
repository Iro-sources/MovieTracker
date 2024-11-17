using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UserManagment.Api.Entities
{


    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserGenreConfig? UserGenreConfig { get; set; }





    }
}
