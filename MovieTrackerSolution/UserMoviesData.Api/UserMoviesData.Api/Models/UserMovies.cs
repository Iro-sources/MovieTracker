namespace UserMoviesData.Api.Models
{
    public class UserMovies
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public string FullTitle { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }
        public string Crew { get; set; }
        public float ImDbRating { get; set; }
        public string ImDbRatingCount { get; set; }
        public string UserId { get; set; }

    }
}
