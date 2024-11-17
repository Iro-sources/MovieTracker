using Auth.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MovieTracker.Web.states.auth.localStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MovieTracker.Web.states.auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        private readonly ILocalStorageService _localStorageService;

        private ClaimsPrincipal _unknown = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }


        // retrieves the current authentication state of the user
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                var authResponse = await _localStorageService.ReadEncryptedItemAsync<AuthResponse>("authResponse");


                if (authResponse is null)
                {
                    return new AuthenticationState(_unknown);
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, authResponse.UserId),
                new Claim(ClaimTypes.Name, authResponse.FirstName),
                new Claim(ClaimTypes.Email, authResponse.Email),
                new Claim(ClaimTypes.Role, authResponse.Role)
                }, "JwtAuth"));

    
                return new AuthenticationState(claimsPrincipal);
            }
            catch
            {
                return new AuthenticationState(_unknown);
            }
        }



        // Login user
        public async Task SetUserAsAuthenticated(AuthResponse authResponse)
        {
            await _localStorageService.SaveItemEncryptedAsync("authResponse", authResponse);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


        // Logout user
        public async Task LoggoutUser()
        {
            await _localStorageService.RemoveItemAsync("authResponse");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        }


        public async Task<string> GetToken()
        {
            string token = string.Empty;
            try
            {
                var storedAuthResponse = await _localStorageService.ReadEncryptedItemAsync<AuthResponse>("authResponse");
                
                if (storedAuthResponse is not null )
                {
                    token = storedAuthResponse.Token;
                }
            }
            catch (Exception ex)
            {

            }
            return token;
        }


        public async Task<string> GetUserId()
        {
            string userId = string.Empty;
            try
            {
                var storedAuthResponse = await _localStorageService.ReadEncryptedItemAsync<AuthResponse>("authResponse");
                if (storedAuthResponse is not null)
                {
                    userId = storedAuthResponse.UserId;
                }
            }
            catch (Exception ex)
            {

            }
            return userId;
        }






    }
}
