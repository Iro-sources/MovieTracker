using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMoviesData.Api.Models
{
    public class UserMovieDbContext: DbContext
    {
        //public UserMovieDbContext() { }

        public UserMovieDbContext(DbContextOptions<UserMovieDbContext> options)
            : base(options)
        {

        }
 
        public DbSet<UserMovies> UserMovies { get; set; }

    }
    
}
