using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models.Dtos.UserMovies.Dtos
{
    public class ExternalSitesDto
    {
            public NetflixDto? Netflix { get; set; }
            public GooglePlayDto? GooglePlay { get; set; }

    }
}
