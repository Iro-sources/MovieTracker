using Auth.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MovieTracker.Models.V2;
using MovieTracker.Web.Services.Contracts;
using MovieTracker.Web.states.auth;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;

namespace MovieTracker.Web.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        IAuthService AuthService { get; set; }

        [Inject]
        HttpClient httpClient { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }


        [Inject]
        NavigationManager NavManager { get; set; }

        [Inject]
        IJSRuntime Js { get; set; }


        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; }



        protected async Task LoginUserAsync()
        {

            try
            {
                var response = await AuthService.Login(new LoginRequest
                {
                    Email = Email,
                    Password = Password,
                });

                if (response.IsSuccessStatusCode)
                {

                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    // Add JWT token to Authorization header for subsequent API requests
            
                    var customAuthStateProvider = (CustomAuthStateProvider)AuthenticationStateProvider;
                    await customAuthStateProvider.SetUserAsAuthenticated(authResponse);
              
                    NavManager.NavigateTo("/", true);
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<Error>();
                    string ErrorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Error";
                    await Js.InvokeVoidAsync("alert", ErrorMessage);
                    return;
                }

            }
            catch (Exception ex)
            {
                await Js.InvokeVoidAsync("alert", "Server error", ex);
                return;
            }

        }
    }
}
