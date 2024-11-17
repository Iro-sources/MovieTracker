namespace MovieTracker.Models.Dtos.UserMovies.Dtos
{
    public class MovieListDto
    {

        public List<MovieDto>? Items { get; set; }
        public string ErrorMessage { get; set; }
    }
}
