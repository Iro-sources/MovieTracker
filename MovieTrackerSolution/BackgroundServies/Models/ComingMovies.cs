namespace BackgroundServies.Models
{
    public class ComingMovies
    {
        public class NewMovieData
        {
            public NewMovieData()
            {
                ErrorMessage = string.Empty;
                Items = new List<NewMovieDataDetail>();
            }

            public NewMovieData(string errorMessage)
            {
                ErrorMessage = errorMessage;
                Items = new List<NewMovieDataDetail>();
            }

            public List<NewMovieDataDetail> Items { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class NewMovieDataDetail
        {

            public string Id { get; set; }
            public string? Title { set; get; }
            public string? FullTitle { set; get; }
            public string? Year { set; get; }
            public string? ReleaseState { set; get; }
            public string? Image { get; set; }
            public string? RuntimeMins { set; get; }
            public string? RuntimeStr { set; get; }
            public string? Plot { set; get; }
            public string? ContentRating { set; get; }
            public string? IMDbRating { set; get; }
            public string? IMDbRatingCount { set; get; }
            public string? MetacriticRating { set; get; }
            public string? Genres { set; get; }
            public string? Directors { set; get; }
            public List<Person>? DirectorList { set; get; }
            public string? Stars { set; get; }
            public List<Person>? StarList { set; get; }
        }
        public class Person
        {
            public int? Id { set; get; }
            public string? Name { set; get; }
        }

    }
}
