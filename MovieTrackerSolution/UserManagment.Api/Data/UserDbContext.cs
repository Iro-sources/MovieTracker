using Microsoft.EntityFrameworkCore;
using UserManagment.Api.Entities;
using UserManagment.Api.Enums;

namespace UserManagment.Api.Data
{
    public class UserDbContext : DbContext
    {


        // passing the options argument/parameter to the base class from which it inherits from Dbcontext class
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var movieGenres = new List<MovieGenre>
            {
                new MovieGenre { Id = 1, Name = Genres.Comedy.ToString() },
                new MovieGenre { Id = 2, Name = Genres.Sci_Fi.ToString().Replace("_", "-") },
                new MovieGenre { Id = 3, Name = Genres.Horror.ToString() },
                new MovieGenre { Id = 4, Name = Genres.Comedy_romance.ToString().Replace("_", "-") },
                new MovieGenre { Id = 5, Name = Genres.Documentary.ToString() },
                new MovieGenre { Id = 6, Name = Genres.Romance.ToString() },
                new MovieGenre { Id = 7, Name = Genres.Drama.ToString() },
                new MovieGenre { Id = 8, Name = Genres.Animation.ToString() },
                new MovieGenre { Id = 9, Name = Genres.Action_Comedy.ToString().Replace("_", "-") },
                new MovieGenre { Id = 10, Name = Genres.Family.ToString() },
                new MovieGenre { Id = 11, Name = Genres.Action.ToString() },
                new MovieGenre { Id = 12, Name = Genres.Mystery.ToString() },
                new MovieGenre { Id = 13, Name = Genres.Adventure.ToString() },
                new MovieGenre { Id = 14, Name = Genres.SuperHero.ToString() },
                new MovieGenre { Id = 15, Name = Genres.History.ToString() },
                new MovieGenre { Id = 16, Name = Genres.Thriller.ToString() },
                new MovieGenre { Id = 17, Name = Genres.Crime.ToString() },
                new MovieGenre { Id = 18, Name = Genres.Fantasy.ToString() },
                new MovieGenre { Id = 19, Name = Genres.Western.ToString() },
            };


            var philipId = new Guid("123e4567-e89b-12d3-a456-426655440000").ToString();
            var stineId = Guid.NewGuid().ToString();
            var robinId = Guid.NewGuid().ToString();
            var mohamedId = Guid.NewGuid().ToString();

            var philipsConfigId = Guid.NewGuid().ToString();
            var stineConfigId = Guid.NewGuid().ToString();
            var robinConfigId = Guid.NewGuid().ToString();
            var mohamedConfigId = Guid.NewGuid().ToString();


            var philip = new User
            {
                Id = philipId,
                FirstName = "Philip",
                LastName = "Fleischer",
                Email = "philip.eiler@hotmail.com",
                Password = "philip",
                CreatedAt = DateTime.Now,
                Role = Enums.Roles.Master.ToString(),


            };


            var stine = new User
            {
                Id = stineId,
                FirstName = "Stine",
                LastName = "Kolsvik",
                Email = "stine@hotmail.com",
                Password = "stine",
                CreatedAt = DateTime.Now,
                Role = Enums.Roles.Admin.ToString(),

            };
            var robin = new User
            {
                Id = robinId,
                FirstName = "Robin",
                LastName = "Dahlman",
                Email = "robin@hotmail.com",
                Password = "robin",
                CreatedAt = DateTime.Now,
                Role = Enums.Roles.User.ToString(),


            };
            var mohamed = new User
            {
                Id = mohamedId,
                FirstName = "Mohamed",
                LastName = "Hassan",
                Email = "mohamed@hotmail.com",
                Password = "mohamed",
                CreatedAt = DateTime.Now,
                Role = Enums.Roles.User.ToString(),


            };

            var philipsConfig = new UserGenreConfig
            {
                Id = philipsConfigId,
                UserId = philipId,

            };

            var stinesConfig = new UserGenreConfig
            {
                Id = stineConfigId,
                UserId = stineId,

            };

            var robinConfig = new UserGenreConfig
            {
                Id = robinConfigId,
                UserId = robinId,

            };


            var mohamedConfig = new UserGenreConfig
            {
                Id = mohamedConfigId,
                UserId = mohamedId,

            };



            // users
            modelBuilder.Entity<User>().HasData(philip);

            modelBuilder.Entity<User>().HasData(stine);

            modelBuilder.Entity<User>().HasData(robin);

            modelBuilder.Entity<User>().HasData(mohamed);

            // movie genres
            modelBuilder.Entity<MovieGenre>().HasData(movieGenres);


            // user configs
            modelBuilder.Entity<UserGenreConfig>().HasData(philipsConfig);
            modelBuilder.Entity<UserGenreConfig>().HasData(stinesConfig);
            modelBuilder.Entity<UserGenreConfig>().HasData(robinConfig);
            modelBuilder.Entity<UserGenreConfig>().HasData(mohamedConfig);

            // config data 


            // philips genres config
            modelBuilder.Entity<ConfigMovieGenre>().HasData(
                new ConfigMovieGenre { UserGenreConfigId = philipsConfigId, MovieGenreId = 1 },
                new ConfigMovieGenre { UserGenreConfigId = philipsConfigId, MovieGenreId = 5 },
                new ConfigMovieGenre { UserGenreConfigId = philipsConfigId, MovieGenreId = 10 },
                new ConfigMovieGenre { UserGenreConfigId = philipsConfigId, MovieGenreId = 2 }

            );


            // stine genres config
            modelBuilder.Entity<ConfigMovieGenre>().HasData(
                new ConfigMovieGenre { UserGenreConfigId = stineConfigId, MovieGenreId = 2 },
                new ConfigMovieGenre { UserGenreConfigId = stineConfigId, MovieGenreId = 3 },
                new ConfigMovieGenre { UserGenreConfigId = stineConfigId, MovieGenreId = 11 },
                new ConfigMovieGenre { UserGenreConfigId = stineConfigId, MovieGenreId = 10 },
                new ConfigMovieGenre { UserGenreConfigId = stineConfigId, MovieGenreId = 19 }

            );


            // robins genres config
            modelBuilder.Entity<ConfigMovieGenre>().HasData(
                new ConfigMovieGenre { UserGenreConfigId = robinConfigId, MovieGenreId = 12 },
                new ConfigMovieGenre { UserGenreConfigId = robinConfigId, MovieGenreId = 3 },
                new ConfigMovieGenre { UserGenreConfigId = robinConfigId, MovieGenreId = 1 },
                new ConfigMovieGenre { UserGenreConfigId = robinConfigId, MovieGenreId = 4 }

            );


            // mohamed genres config
            modelBuilder.Entity<ConfigMovieGenre>().HasData(
                new ConfigMovieGenre { UserGenreConfigId = mohamedConfigId, MovieGenreId = 1 },
                new ConfigMovieGenre { UserGenreConfigId = mohamedConfigId, MovieGenreId = 2 },
                new ConfigMovieGenre { UserGenreConfigId = mohamedConfigId, MovieGenreId = 3 },
                new ConfigMovieGenre { UserGenreConfigId = mohamedConfigId, MovieGenreId = 17 }

            );






            modelBuilder.Entity<ConfigMovieGenre>().HasKey(cmg => new
            {
                cmg.UserGenreConfigId,
                cmg.MovieGenreId
            });


        }







        public DbSet<User> Users { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<UserGenreConfig> UserGenreConfigs { get; set; }
        public DbSet<ConfigMovieGenre> ConfigMovieGenres { get; set; }




    }
}
