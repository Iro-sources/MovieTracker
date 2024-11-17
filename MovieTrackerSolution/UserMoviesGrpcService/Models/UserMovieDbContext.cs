using Microsoft.EntityFrameworkCore;

namespace UserMoviesGrpcService.Models
{
    public class UserMovieDbContext: DbContext
    {
        public UserMovieDbContext() { }

        public UserMovieDbContext(DbContextOptions<UserMovieDbContext> options)
            : base(options)
        {

        }
 
        public DbSet<UserMovies> UserMovies { get; set; }

    }
    
}
