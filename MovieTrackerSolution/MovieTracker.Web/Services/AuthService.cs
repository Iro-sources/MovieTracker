using Auth.Models;
using MovieTracker.Web.Services.Contracts;
using System.Net.Http.Json;

namespace MovieTracker.Web.Services
{
    public class AuthService : IAuthService
    {


        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7194/api/auth/login";


        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_url),

            };

        }

        public async Task<HttpResponseMessage> Login(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(_url, loginRequest);
            return response;
        }
    }
}
