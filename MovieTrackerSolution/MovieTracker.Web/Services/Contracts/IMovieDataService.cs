using MovieTracker.Models.Dtos.MovieRecommendation.Dtos;
using MovieTracker.Models.Dtos.MoviesData.API;
using MovieTracker.Models.Dtos.UserMovies.Dtos;
using System.Threading.Tasks;

namespace MovieTracker.Web.Services.Contracts
{
    public interface IMovieDataService
    {
        public Task<MovieListDto> GetMovies();
        public Task<MovieDetailDto> GetMovieById(string id);

        public Task<MovieTrailerDto> GetMovieTrailer(string id);

        public Task<ExternalSitesDto> GetExternalSites(string id);

        public Task<ReviewListDTO> GetReviewList(string id);


        public Task<IEnumerable<AdvanceSearchMovieDto>> MovieRecommendation(List<string> selectedGenre);

        public Task<InTheatreListDTO> GetInTheatres();
        public  Task<List<AdvanceSearchMovieDto>> GetMoviesByAdvanceFilterV2(string title, string year, string genres);
    }
}
