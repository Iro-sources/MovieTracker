using MovieTracker.Models.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.MoviesData.API
{
    public class AdvanceSearchMoviesDto
    {
            public string QueryString { get; set; }
            public List<ResultDto> Results { get; set; }
            public string ErrorMessage { get; set; }
        
    }
}
