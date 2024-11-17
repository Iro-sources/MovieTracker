using System.ComponentModel.DataAnnotations;

namespace UserManagment.Api.Entities
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<ConfigMovieGenre> ConfigMovieGenres { get; set; }
    }
}
