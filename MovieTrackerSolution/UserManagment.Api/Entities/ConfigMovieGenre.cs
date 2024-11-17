using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagment.Api.Entities
{
    public class ConfigMovieGenre
    {

        [ForeignKey("ConfigMovieGenre")]
        public string UserGenreConfigId { get; set; }
        public UserGenreConfig UserGenreConfig { get; set; }


        public int MovieGenreId { get; set; }
        public MovieGenre MovieGenre { get; set; }
    }
}
