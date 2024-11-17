using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.UserMovies.Dtos
{
    public class ReviewDTO
    {
        public string? UserName { get; set; }
        public string? Date { get; set; }
        public bool? WarningSpoilers { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public string? Year { get; set; }

        public string? UserUrl { get; set; }

        public string? Rate { get; set; }

    }
}
