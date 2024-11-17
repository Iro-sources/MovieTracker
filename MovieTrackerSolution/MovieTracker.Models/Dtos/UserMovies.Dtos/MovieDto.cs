using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.UserMovies.Dtos
{
    public class MovieDto
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
    }
}
