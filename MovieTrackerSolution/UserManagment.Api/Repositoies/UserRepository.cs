using Microsoft.EntityFrameworkCore;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using UserManagment.Api.Data;
using UserManagment.Api.Entities;
using UserManagment.Api.Enums;
using UserManagment.Api.Repositoies.Contracts;

namespace UserManagment.Api.Repositoies
{
    public class UserRepository : IUserRepository
    {
        //injected with DI
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

        }
        public async Task<IEnumerable<User>> GetUsers()
        {

            var users = await _userDbContext.Users
           .Include(u => u.UserGenreConfig)
           .ThenInclude(ugc => ugc.ConfigMovieGenres)
           .Select(u => new User
           {
               Id = u.Id,
               FirstName = u.FirstName,
               LastName = u.LastName,
               Password = u.Password,
               Email = u.Email,
               Role = u.Role,
               CreatedAt = u.CreatedAt,
               UserGenreConfig = u.UserGenreConfig == null ? null : new UserGenreConfig // if UserGenreConfig is null -> return null
               {
                   Id = u.UserGenreConfig.Id,
                   UserId = u.UserGenreConfig.UserId,
                   ConfigMovieGenres = (ICollection<ConfigMovieGenre>)u.UserGenreConfig.ConfigMovieGenres.Select(cmg => new ConfigMovieGenre
                   {
                       MovieGenreId = cmg.MovieGenreId,
                       MovieGenre = cmg.MovieGenre,
                   })

               }
           })
           .ToListAsync();

            return users;
        }



        public async Task<User?> GetUserById(string id)
        {
            var user = await _userDbContext.Users
           .Include(u => u.UserGenreConfig.ConfigMovieGenres)
           .Where(u => u.Id == id)
           .Select(u => new User
           {
               Id = u.Id,
               FirstName = u.FirstName,
               LastName = u.LastName,
               Password = u.Password,
               Email = u.Email,
               Role = u.Role,
               CreatedAt = u.CreatedAt,
               UserGenreConfig = u.UserGenreConfig != null ? new UserGenreConfig
               {
                   Id = u.UserGenreConfig.Id,
                   UserId = u.UserGenreConfig.UserId,
                   ConfigMovieGenres = u.UserGenreConfig.ConfigMovieGenres.Select(cmg => new ConfigMovieGenre
                   {
                       MovieGenreId = cmg.MovieGenreId,
                       MovieGenre = cmg.MovieGenre
                   }).ToList()
               } : null
           })
             .FirstOrDefaultAsync();
            if (user is null)
            {
                return null;

            }
            return user;
        }



        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _userDbContext.Users
                .Include(u => u.UserGenreConfig)
                .ThenInclude(ugc => ugc.ConfigMovieGenres)
                .Select(u => new User
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Password = u.Password,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    UserGenreConfig = u.UserGenreConfig == null ? null : new UserGenreConfig // if UserGenreConfig is null -> return null
                    {
                        Id = u.UserGenreConfig.Id,
                        UserId = u.UserGenreConfig.UserId,
                        ConfigMovieGenres = (ICollection<ConfigMovieGenre>)u.UserGenreConfig.ConfigMovieGenres.Select(cmg => new ConfigMovieGenre
                        {
                            MovieGenreId = cmg.MovieGenreId,
                            MovieGenre = cmg.MovieGenre,
                        })

                    }
                })
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                return null;
            }
            return user;
        }


        public async Task UpdateUserGenrePreference(string userId, Dictionary<string, bool> preferences)
        {
            // get user object
            var user = await _userDbContext.Users
                  .Include(u => u.UserGenreConfig)
                  .ThenInclude(ugc => ugc.ConfigMovieGenres).FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                return;
            }

            // if user dont have a config we create one
            if (user.UserGenreConfig == null)
            {
                user.UserGenreConfig = new UserGenreConfig();
            }


            // making the new config list
            ICollection<ConfigMovieGenre> configMovieGenres = new List<ConfigMovieGenre>();

            // loop through the Dictionary containig the user's new config preference
            foreach (var preference in preferences)
            {
                // check if we should add the genre
                if (preference.Value)
                {

                    var genre = await _userDbContext.MovieGenres
                        .SingleOrDefaultAsync(genre => genre.Name == preference.Key);

                    // if the genre exist
                    if (genre is not null)
                    {
                        var configMovieGenre = new ConfigMovieGenre
                        {
                            MovieGenre = genre
                        };

                        // add to new config list
                        configMovieGenres.Add(configMovieGenre);
                    }
                }
            }


            // clear the old pref list
            user.UserGenreConfig.ConfigMovieGenres.Clear();

            // add the new one pref list
            user.UserGenreConfig.ConfigMovieGenres = configMovieGenres;


            await _userDbContext.SaveChangesAsync();

            return;
        }

        public async Task<UserSignUpDto> CreateNewUser(UserSignUpDto userSignUpDto)
        {

            var userGuid = Guid.NewGuid().ToString();
            ICollection<ConfigMovieGenre> configMovieGenres = new List<ConfigMovieGenre>();

            foreach (var genre in userSignUpDto.GenreConfig)
            {
                // when genre is true (Drama => true)
                if (genre.Value)
                {
                    var key = genre.Key;

                    var checkIfGenreExist = await _userDbContext.MovieGenres
                        .SingleOrDefaultAsync(genre => genre.Name == key);


                    // if the genre exist
                    if (checkIfGenreExist is not null)
                    {
                        var configMovieGenre = new ConfigMovieGenre
                        {
                            MovieGenre = checkIfGenreExist
                        };

                        // add to new config list
                        configMovieGenres.Add(configMovieGenre);
                    }

                }

            }


            var user = _userDbContext.Users.Add(new User
            {
                Id = userGuid,
                FirstName = userSignUpDto.FirstName,
                LastName = userSignUpDto.LastName,
                Email = userSignUpDto.Email,
                Password = userSignUpDto.Password,
                Role = Roles.User.ToString(),
                CreatedAt = DateTime.Now,
                UserGenreConfig = new UserGenreConfig
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userGuid,
                    ConfigMovieGenres = configMovieGenres

                }




            });

            await _userDbContext.SaveChangesAsync();

            if (user is not null)
            {
                return userSignUpDto;
            }
            else
            {
                return null;
            }




        }

        public Task<IEnumerable<ConfigMovieGenre>> GetUserMovieGenreConfig(string userId)
        {
            throw new NotImplementedException();
        }
    }


}
