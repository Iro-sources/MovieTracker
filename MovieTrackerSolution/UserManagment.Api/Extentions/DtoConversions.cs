using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using UserManagment.Api.Entities;

namespace UserManagment.Api.Extentions
{
    public static class DtoConversions
    {
        public static IEnumerable<UserReadDto> ConvertToDto(this IEnumerable<User> users)
        {
            return (from user in users
                    select new UserReadDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        UserEmail = user.Email,
                        CreatedAt = user.CreatedAt,
                        Role = user.Role,
                        UserGenreConfigId = user.UserGenreConfig.Id,
                        SubscribedGenres = user.UserGenreConfig.ConfigMovieGenres.Select(cmg => cmg.MovieGenre.Name).ToList()
                    }).ToList();

        }


        public static UserReadDto ConvertToDto(this User user)
        {
            return new UserReadDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                UserEmail = user.Email,
                CreatedAt = user.CreatedAt,
                Role = user.Role,
                UserGenreConfigId = user.UserGenreConfig.Id,
                SubscribedGenres = user.UserGenreConfig.ConfigMovieGenres.Select(cmg => cmg.MovieGenre.Name).ToList()
            };

        }

    }
}
