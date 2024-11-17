using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.UserMovies.Dtos
{
    public class MovieDetailDto
    {
        public string? Id { get; set; }
        public int? Rank { get; set; }
        public string? Writers { get; set; }
        public string? Directors { get; set; }
        public string? Title { get; set; }
        public string? RunTime { get; set; }
        public string? Plot { get; set; }
        public int? Year { get; set; }
        public string? Image { get; set; }
        public string? Crew { get; set; }
        public string? Awards { get; set; }
        public float? ImDbRating { get; set; }
        public List<StarDto> StarList { get; set; }

        public List<SimilarMoviesDto> Similars { get; set; }

    }
}
