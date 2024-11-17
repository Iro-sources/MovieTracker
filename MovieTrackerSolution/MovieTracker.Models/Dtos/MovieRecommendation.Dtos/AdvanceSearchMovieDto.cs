using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.MovieRecommendation.Dtos
{
    public class AdvanceSearchMovieDto
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string description { get; set; }

        public string runtimeStr { get; set; }

        public string imDbRating { get; set; }

        public string Genres { get; set; }
    }


}
