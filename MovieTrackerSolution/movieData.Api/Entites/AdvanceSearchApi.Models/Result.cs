using System.Diagnostics;

namespace MovieData.Api.Entites.AdvanceSearchApi.Models
{
    public class Result
    {
        public string id { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string runtimeStr { get; set; }
        public string genres { get; set; }
        public List<GenreList> genreList { get; set; }
        public string contentRating { get; set; }
        public string imDbRating { get; set; }
        public string imDbRatingVotes { get; set; }
        public string metacriticRating { get; set; }
        public string plot { get; set; }
        public string stars { get; set; }
        public List<StarList> starList { get; set; }
    }
}
