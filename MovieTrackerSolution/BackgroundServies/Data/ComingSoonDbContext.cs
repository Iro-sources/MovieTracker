using BackgroundServies.Models;
using Microsoft.EntityFrameworkCore;

namespace BackgroundServies.Data
{
    public class ComingSoonDbContext : DbContext
    {
        public DbSet<ComingMovies.NewMovieDataDetail> Movies { get; set; }
        public DbSet<ComingMovies.Person> People { get; set; }

        public ComingSoonDbContext(DbContextOptions<ComingSoonDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method to ensure any default configuration is applied
            base.OnModelCreating(modelBuilder);

            // Configure the entity mappings
            modelBuilder.Entity<ComingMovies.NewMovieDataDetail>(entity =>
            {
                // Configure the table name if it differs from the entity name
                entity.ToTable("Movies");

                // Configure primary key
                entity.HasKey(m => m.Id);

                // Configure other properties
                entity.Property(m => m.Title).HasMaxLength(100);
                entity.Property(m => m.FullTitle).HasMaxLength(100);
                entity.Property(m => m.Year);
                entity.Property(m => m.ReleaseState);
                entity.Property(m => m.Image);
                entity.Property(m => m.RuntimeMins);
                entity.Property(m => m.RuntimeStr);
                entity.Property(m => m.Plot);
                entity.Property(m => m.ContentRating);
                entity.Property(m => m.IMDbRating);
                entity.Property(m => m.IMDbRatingCount);
                entity.Property(m => m.MetacriticRating);
                entity.Property(m => m.Genres);

                // Configure the relationship with the Person entity for directors
                entity.HasMany(m => m.DirectorList)
                    .WithOne()
                    .HasForeignKey("DirectorId"); // Add this line to specify the foreign key

                // Configure the relationship with the Person entity for stars
                entity.HasMany(m => m.StarList)
                    .WithOne()
                    .HasForeignKey("StarId"); // Add this line to specify the foreign key
            });

            // Configure the entity mappings for Person
            modelBuilder.Entity<ComingMovies.Person>(entity =>
            {
                entity.ToTable("People");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).HasMaxLength(100);
            });
        }
    }
}
