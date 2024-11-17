using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.MoviesData.API
{
    public class ResultDto
    {
        public string? id { get; set; }
        public string? image { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? runtimeStr { get; set; }
        public string? genres { get; set; }
        public List<GenreListDto>? genreList { get; set; }
        public string? contentRating { get; set; }
        public string? imDbRating { get; set; }
        public string? imDbRatingVotes { get; set; }
        public string? metacriticRating { get; set; }
        public string? plot { get; set; }
        public string? stars { get; set; }
        public List<StarListDto>? starList { get; set; }

    }
}
