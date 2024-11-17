using MovieTracker.Models.Dtos.UserMovies.Dtos;
using MovieTracker.Models.Dtos.UserMoviesData.Api;

namespace MovieTracker.Web.Services.Contracts
{
    public interface IUserMoviesService
    {
        public Task<MovieListDto> PostUserMoviesData(UserMoviesDto data);
        public Task<List<UserMoviesDto>> GetUserMoviesData(string user);
        public Task<bool> DeleteUserMovie(string movieId);
    }
}
