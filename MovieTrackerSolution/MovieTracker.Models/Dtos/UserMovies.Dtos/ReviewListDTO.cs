using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.UserMovies.Dtos
{
    public class ReviewListDTO
    {
        public List<ReviewDTO>? Items { get; set; }

        public string? Title { get; set; }
    }
}
