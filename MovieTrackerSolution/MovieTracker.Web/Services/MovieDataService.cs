using MovieTracker.Models.Dtos.MovieRecommendation.Dtos;
using MovieTracker.Models.Dtos.MoviesData.API;
using MovieTracker.Models.Dtos.UserMovies.Dtos;
using MovieTracker.Models.V2;
using MovieTracker.Web.Services.Contracts;
using System.Net.Http.Json;

namespace MovieTracker.Web.Services
{
    public class MovieDataService : IMovieDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7222";

        public MovieDataService()
        {
            _httpClient = new HttpClient();

        }
        //api/2.0/topmovies
        public async Task<MovieListDto> GetMovies()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/api/2.0/topmovies?numberOfMovies=100");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<MovieListDto>();
                    return data;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }

        public async Task<MovieDetailDto?> GetMovieById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/api/2.0/movies/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<MovieDetailDto>();

                    return data;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }

        public async Task<MovieTrailerDto?> GetMovieTrailer(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/api/2.0/movies/{id}/youtubetrailer");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<MovieTrailerDto>();

                    return data;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }

        public async Task<ExternalSitesDto?> GetExternalSites(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/api/2.0/movies/{id}/externalsites");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<ExternalSitesDto>();

                    return data;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }

        public async Task<ReviewListDTO?> GetReviewList(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/api/2.0/movies/{id}/reviews");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<ReviewListDTO>();

                    return data;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }


    
        public async Task<IEnumerable<AdvanceSearchMovieDto>> MovieRecommendation(List<string> selectedGenre)
        {
            try
            {

                var response = await _httpClient.GetAsync($"{_url}/api/2.0/AdvancedSearch?genres={string.Join(",", selectedGenre)}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<IEnumerable<AdvanceSearchMovieDto>>();
                    return data;
                }
     
                return null;

            } catch(HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }

        public async Task<InTheatreListDTO> GetInTheatres()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/api/2.0/InTheaters");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadFromJsonAsync<InTheatreListDTO>();

                    return data;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }

        public async Task<List<AdvanceSearchMovieDto>> GetMoviesByAdvanceFilterV2(string title, string year, string genres)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<AdvanceSearchMovieDto>>($"{_url}/api/2.0/AdvancedSearch?release_date={year}&title={title}&genres={genres}");

                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    var data = await response.Content.ReadFromJsonAsync<AdvanceSearchMoviesDto>();
                //    return data;
                //}
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }
    }
}
