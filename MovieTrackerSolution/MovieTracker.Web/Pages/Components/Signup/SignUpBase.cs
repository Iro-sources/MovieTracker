using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V2;
using MovieTracker.Web.Services.Contracts;
using MovieTracker.Web.Shared;
using MovieTracker.Web.states.auth;
using System.Net.Http.Json;

namespace MovieTracker.Web.Pages.Components.Signup
{
    public class SignUpBase : ComponentBase
    {

        [Inject]
        private IJSRuntime Js { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }

        public List<string> GenresList = new List<string> {
        "Comedy",
        "Sci-Fi",
        "Horror",
        "Comedy-romance",
        "Documentary",
        "Romance",
        "Drama",
        "Animation",
        "Action-Comedy",
        "Family",
        "Action",
        "Mystery",
        "Adventure",
        "History",
        "Thriller",
        "Crime",
        "Fantasy",
        "Western"
        };

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        public List<string> SelectedGenres { get; set; } = new List<string>();



        public async void OnCheckBoxChanged(string genre)
        {
           if(SelectedGenres.Contains(genre))
           {
                 SelectedGenres.Remove(genre);
           }
           else
            {
                SelectedGenres.Add(genre);
            }           
        }


        protected async Task SignUpNewUser()
        {
            if(!SelectedGenres.Any())
            {
                await Js.InvokeVoidAsync("alert", "Hey, You need to atleast pick one genre!");
                return;
            }
            var preferences = new Dictionary<string, bool>();
            foreach (var genre in SelectedGenres)
            {
                preferences[genre] = true;
            }

            var newUser = new UserSignUpDto
            {
                FirstName= FirstName,
                LastName= LastName,
                Email= Email,
                Password= Password,
                GenreConfig= preferences

            };


            var response = await UserService.CreateNewUser(newUser);

            if(response.IsSuccessStatusCode)
            {
                await Js.InvokeAsync<object>("alert", "Succsess you have created an account!");
                NavManager.NavigateTo("/Login");
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<Error>();
                string ErrorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Error";
                await Js.InvokeVoidAsync("alert", ErrorMessage);
           
            }
        }
    }
}
