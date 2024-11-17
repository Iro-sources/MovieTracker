using MovieData.Api.Entites.AdvanceSearchApi.Models;
using MovieTracker.Models.Dtos.MovieRecommendation.Dtos;

namespace MovieData.Api.Extentions
{
    public static class DtoConversions
    {



        public static IEnumerable<AdvanceSearchMovieDto> ConvertToDto(this AdvanceSearchMovies movies)
        {
            return (from movie in movies.Results
                   select new AdvanceSearchMovieDto
                   {
                       Id = movie.id,
                       Title= movie.title,
                       Image   = movie.image,
                       description= movie.description,
                       runtimeStr= movie.runtimeStr,
                       imDbRating= movie.imDbRating,
                       Genres= movie.genres,
                   }).ToList();
        } 
    }
}
