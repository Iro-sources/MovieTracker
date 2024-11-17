using Grpc.Core;
using UserMoviesGrpcService.Models;

namespace UserMoviesGrpcService.Services
{
    public class MovieServiceImp : MovieService.MovieServiceBase
    {
        private readonly ILogger<MovieServiceImp> _logger;
        private UserMovieDbContext _context;

        public MovieServiceImp(ILogger<MovieServiceImp> logger, UserMovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<UserMoviesResponse> GetUserMovies(UserMoviesRequest request, ServerCallContext context)
        {
            var result = _context.UserMovies.Where(x => x.UserId == request.UserId.ToString()).ToList();

            var movies = new List<UserMovie> {
                new UserMovie()
                {
                    Id = "id-1",
                    FullTitle ="Movie 1 Full Title",
                    Crew ="Crw details goes here",
                    ImDbRating = 4,
                    ImDbRatingCount = "26",
                    Rank = 1,
                    Title = "Movice 1",
                    UserId = "1a87c963-3cdf-46c1-99cd-5f07bad00190"

                },
                new UserMovie()
                {
                    Id = "id-2",
                    FullTitle ="Movie 2 Full Title",
                    Crew ="Crw details goes here",
                    ImDbRating = 4,
                    ImDbRatingCount = "16",
                    Rank = 1,
                    Title = "Movice 2",
                    UserId = "1a87c963-3cdf-46c1-99cd-5f07bad00191"

                }
            };
            //var result = movies.Where(x => x.UserId == request.UserId.ToString());
            var response = new UserMoviesResponse();
            response.UserMovies.AddRange(result.Select(um => new UserMovie
            {
                Id = um.Id,
                Rank = um.Rank,
                Title = um.Title,
                FullTitle = um.FullTitle,
                Year = um.Year,
                Image = um.Image,
                Crew = um.Crew,
                ImDbRating = um.ImDbRating,
                ImDbRatingCount = um.ImDbRatingCount
            }));

            return Task.FromResult(response);
        }

        public override Task<UserMovieResponse> AddUserMovie(UserMovieRequest request, ServerCallContext context)
        {
            var response = new UserMovieResponse();

            _context.UserMovies.Add(new UserMovies
            {
                Id = request.UserMovies.Id,
                Title = request.UserMovies.Title,
                Crew = request.UserMovies.Crew,
                FullTitle = request.UserMovies.FullTitle,
                Image = request.UserMovies.Image,
                ImDbRating = request.UserMovies.ImDbRating,
                ImDbRatingCount = request.UserMovies.ImDbRatingCount,
                Rank = request.UserMovies.Rank,
                UserId = request.UserMovies.UserId,
                Year = request.UserMovies.Year
            });
            response.Saved = _context.SaveChanges();

            return Task.FromResult(response);
        }

        public override Task<UserMovieDeleteResponse> DeleteUserMovie(UserMovieDeleteRequest request, ServerCallContext context)
        {
            var response = new UserMovieDeleteResponse();
            var movie = _context.UserMovies.FirstOrDefault(x => x.Id == request.MovieId);
            if (movie != null)
                _context.UserMovies.Remove(movie);
            response.Deleted = _context.SaveChanges();

            return Task.FromResult(response);
        }
    }
}