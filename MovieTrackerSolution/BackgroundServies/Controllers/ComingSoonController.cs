using BackgroundServies.Api.configurations;
using BackgroundServies.Api.Service;
using BackgroundServies.Data;
using BackgroundServies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;


namespace BackgroundServies
{
    public class MovieUpdateService : BackgroundService
    {
        private readonly IRestClientService _restClient;
        private readonly IOptions<ApiKeyConfiguration> _apiKeyConfiguration;
        private readonly ILogger<MovieUpdateService> _logger;
        private readonly ComingSoonDbContext _dbContext;

        public MovieUpdateService(
            IRestClientService restClientService,
            IOptions<ApiKeyConfiguration> apiKey,
            ILogger<MovieUpdateService> logger,
            ComingSoonDbContext dbContext)
        {
            _restClient = restClientService;
            _apiKeyConfiguration = apiKey;
            _logger = logger;
            _dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Use the IRestClientService to make the API request
                    var response = await _restClient.Get($"/API/ComingSoon/{_apiKeyConfiguration.Value.ImdbApiKey}");

                    if (response.IsSuccessStatusCode)
                    {
                        var moviesData = JsonConvert.DeserializeObject<ComingMovies.NewMovieData>(response.Content);

                        /* FOR TESTING!
                        // Write the movie data to a JSON file
                        var json = JsonConvert.SerializeObject(moviesData, Formatting.Indented);
                        await File.WriteAllTextAsync("movies.json", json);
                        _logger.LogInformation("Saved API Request to JSON Locally");
                        */

                        foreach (var movie in moviesData.Items)
                        {
                            // Check if the movie already exists in the database
                            var existingMovie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movie.Id);

                            if (existingMovie != null)
                            {
                                // Update existing movie
                                existingMovie.Title = movie.Title;
                                existingMovie.FullTitle = movie.FullTitle;
                                existingMovie.Year = movie.Year;
                                existingMovie.ReleaseState = movie.ReleaseState;
                                existingMovie.Image = movie.Image;
                                existingMovie.RuntimeMins = movie.RuntimeMins;
                                existingMovie.RuntimeStr = movie.RuntimeStr;
                                existingMovie.Plot = movie.Plot;
                                existingMovie.ContentRating = movie.ContentRating;
                                existingMovie.IMDbRating = movie.IMDbRating;
                                existingMovie.IMDbRatingCount = movie.IMDbRatingCount;
                                existingMovie.MetacriticRating = movie.MetacriticRating;
                                existingMovie.Genres = movie.Genres;
                                existingMovie.Directors = movie.Directors;
                                existingMovie.Stars = movie.Stars;
                                existingMovie.DirectorList = movie.DirectorList;
                                existingMovie.StarList = movie.StarList;

                                // Update other properties as needed

                                _dbContext.Movies.Update(existingMovie);
                            }

                            else
                            {
                                // Create a new movie
                                var newMovie = new ComingMovies.NewMovieDataDetail
                                {
                                    Id = movie.Id,
                                    Title = movie.Title,
                                    FullTitle = movie.FullTitle,
                                    Year = movie.Year,
                                    ReleaseState = movie.ReleaseState,
                                    Image = movie.Image,
                                    RuntimeMins = movie.RuntimeMins,
                                    RuntimeStr = movie.RuntimeStr,
                                    Plot = movie.Plot,
                                    ContentRating = movie.ContentRating,
                                    IMDbRating = movie.IMDbRating,
                                    IMDbRatingCount = movie.IMDbRatingCount,
                                    MetacriticRating = movie.MetacriticRating,
                                    Genres = movie.Genres,
                                    Directors = movie.Directors,
                                    Stars = movie.Stars,
                                    DirectorList = movie.DirectorList,
                                    StarList = movie.StarList
                                };


                                _dbContext.Movies.Add(newMovie);
                            }
                        }

                        await _dbContext.SaveChangesAsync();




                        _logger.LogInformation("Movie data updated successfully.");
                    }
                    else
                    {
                        // Handle the API request failure
                        _logger.LogError($"API request failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the request
                    _logger.LogError($"Error occurred: {ex.Message}");
                }

                // Wait for a specified interval before the next update
                // 1440 minutes for a full day
                await Task.Delay(TimeSpan.FromMinutes(1440), stoppingToken);
            }
        }

        [HttpGet]
        [Route("api/ComingSoon")]
        public async Task<ActionResult<List<ComingMovies.NewMovieDataDetail>>> GetMovies()
        {
            var movies = await _dbContext.Movies.ToListAsync();

            return movies;
        }

    }
}