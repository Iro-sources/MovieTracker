
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using MovieData.Api.Entites.MoviesApi.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Xunit;
using Xunit.Sdk;

namespace MovieData.Api.Tests.Integration;

//public class MovieDataFactory : WebApplicationFactory<Program>
//{

//    protected override void ConfigureWebHost(IWebHostBuilder builder)
//    {
//        builder.ConfigureTestServices(services =>
//        {

//        });
//    }

//}
public class MovieDataFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            //// Register the DbContextOptions as Singleton
            //services.AddSingleton(dbContextOptions);

            //services.AddDbContext<TDbContext>(options => options.UseSqlServer(_dbContainer.GetConnectionString()));
            //services.EnsureDbCreated<TDbContext>();
        });
    }
}
public class V2MovieControllerTests
{
    public sealed class Api : IClassFixture<V2MovieControllerTests>, IDisposable
    {
        private readonly MovieDataFactory<Program> _movieDataFactory;
        private readonly IServiceScope _serviceScope;
        private readonly HttpClient _httpClient;

        public Api(V2MovieControllerTests v2MovieControllerTests)
        {
            _movieDataFactory = new MovieDataFactory<Program>();
            _serviceScope = _movieDataFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _httpClient = _movieDataFactory.CreateClient();
        }
        public void Dispose()
        {
            _httpClient.Dispose();
            _serviceScope.Dispose();
            _movieDataFactory.Dispose();
        }

        [Fact]
        public async Task GetTen_Movies_ReturnsTenMovies()
        {
            // Given
            int numberOfMovies = 10;
            string path = $"api/2.0/topmovies?numberOfMovies={numberOfMovies}";

            // When
            var response = await _httpClient.GetAsync(path);

            var stream = await response.Content.ReadAsStringAsync();

            var movies = JsonConvert.DeserializeObject<MovieList>(stream);


            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(numberOfMovies, movies.Items.Count);
        }

        [Fact]
        public async Task GetMoviesByAdvanceSearch_ReturnsOk()
        {
            // Given
            var genres = new List<string> { "Action", "Drama" };
            var releaseDate = "2022";
            var title = "Movie";
            string path = $"api/2.0/AdvancedSearch?genres={string.Join(",", genres)}&release_date={releaseDate}&title={title}";

            // When
            var response = await _httpClient.GetAsync(path);

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMovieById_ReturnsOk()
        {
            // Given
            var id = "tt1234567";
            string path = $"api/2.0/movies/{id}";

            // When
            var response = await _httpClient.GetAsync(path);

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetMovieTrailer_ReturnsOk()
        {
            // Given
            var id = "tt1234567";
            string path = $"api/2.0/movies/{id}/youtubetrailer";

            // When
            var response = await _httpClient.GetAsync(path);

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAwardsWonByMovie_ReturnsOk()
        {
            // Given
            var id = "tt1234567";
            string path = $"api/2.0/movies/{id}/movieAwards";

            // When
            var response = await _httpClient.GetAsync(path);

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetExternalSites_ReturnsOk()
        {
            // Given
            var id = "tt1234567";
            string path = $"api/2.0/movies/{id}/externalSites";

            // When
            var response = await _httpClient.GetAsync(path);

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetReviews_ReturnsOk()
        {
            // Given
            var id = "tt1234567";
            string path = $"api/2.0/movies/{id}/reviews";

            // When
            var response = await _httpClient.GetAsync(path);

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetInTheatres_ReturnsOk()
        {
            // Given
            string path = "api/2.0/InTheaters";

            // When
            var response = await _httpClient.GetAsync(path);

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}