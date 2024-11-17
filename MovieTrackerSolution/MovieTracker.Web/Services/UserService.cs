using Microsoft.AspNetCore.Components;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V2;
using MovieTracker.Web.Services.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace MovieTracker.Web.Services
{
    public class UserService : IUserService
    {


        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7114";



        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_url),

            };

        }

        public async Task<HttpResponseMessage> CreateNewUser(UserSignUpDto userSignUpDto)
        {
         
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_url}/api/2.0/users", userSignUpDto);

                return response;
            


                

            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }
        }

        public async Task<UserReadDto?> GetUser(string Id, string Token)
        {

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var response = await _httpClient.GetAsync($"https://localhost:7238/api/UserManagmentAuthGateway/users/{Id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var user = await response.Content.ReadFromJsonAsync<Content<UserReadDto>>();

                    return user != null ? user.Data : null;

                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<Error>();
                    string errorMsg = errorResponse != null ? errorResponse.ErrorMessage : "Error";
                    throw new Exception(errorMsg);
                }

            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to connect to the API.", ex);
            }

        }

        public async Task<HttpResponseMessage> UpdateUserGenrePreferences(string userId, Dictionary<string, bool> preferences)
        {
          
            var response = await _httpClient.PostAsJsonAsync($"{_url}/api/2.0/users/{userId}/config/genrePreference", preferences);
            return response;
     
        }
    }
}
