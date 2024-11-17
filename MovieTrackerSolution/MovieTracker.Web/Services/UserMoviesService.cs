using MovieTracker.Models.Dtos.UserMovies.Dtos;
using MovieTracker.Models.Dtos.UserMoviesData.Api;
using MovieTracker.Web.Services.Contracts;
using System.Net.Http.Json;

namespace MovieTracker.Web.Services
{
    public class UserMoviesService : IUserMoviesService
    {

        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7280";

        public UserMoviesService()
        {
            _httpClient = new HttpClient();

        }

        public async Task<MovieListDto> PostUserMoviesData(UserMoviesDto movie)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<UserMoviesDto>($"{_url}/api/userMovies/addUserMovie", movie);

                if(response.IsSuccessStatusCode)
                {

                }

                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }


        // get movies of current user


        public async Task<List<UserMoviesDto>> GetUserMoviesData(string user)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<UserMoviesDto>>($"{_url}/api/userMovies/getUserMovies/{user}");
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }



        // delete movie of current user
        public async Task<bool> DeleteUserMovie(string movieId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_url}/api/userMovies/deleteMovie/{movieId}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }
    }
}
