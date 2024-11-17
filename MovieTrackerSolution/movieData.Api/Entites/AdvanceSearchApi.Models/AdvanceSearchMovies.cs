using MovieTracker.Models.V2;

namespace MovieData.Api.Entites.AdvanceSearchApi.Models
{
    public class AdvanceSearchMovies
    {
        public string QueryString { get; set; }
        public List<Result> Results { get; set; }
        public string ErrorMessage { get; set; }
    }
}
