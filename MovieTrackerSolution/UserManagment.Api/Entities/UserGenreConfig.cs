using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagment.Api.Entities
{

    public class UserGenreConfig
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<ConfigMovieGenre> ConfigMovieGenres { get; set; }

    }
}
