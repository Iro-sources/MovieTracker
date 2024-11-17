using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V2;
using MovieTracker.Web.Pages.Components;
using MovieTracker.Web.Services.Contracts;
using MovieTracker.Web.states.auth;
using System.Net.Http.Json;
using System.Reflection;

namespace MovieTracker.Web.Pages.Profile
{
    public class ProfileBase : ComponentBase
    {

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IJSRuntime Js { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }

        public bool _ShowModel = false;

        public string ErrorMessage { get; set; }

        public UserReadDto User { get; set; }

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

        [Parameter]
        public List<string> SelectedGenres { get; set; } = new List<string>();

        protected async Task OpenModal()
        {
            SelectedGenres.Clear();
            SelectedGenres = User.SubscribedGenres.ToList();
            _ShowModel = !_ShowModel;
        }

        protected async Task SavePreferences()
        {

            var preferences = new Dictionary<string, bool>();
            foreach (var genre in SelectedGenres)
            {
                preferences[genre] = true;
            }
         
            //update user pref
           var response = await UserService.UpdateUserGenrePreferences(User.Id, preferences);

            if(response.IsSuccessStatusCode)
            {
                var customAuthStateProvider = (CustomAuthStateProvider)AuthenticationStateProvider;
                var token = await customAuthStateProvider.GetToken();
                User = await UserService.GetUser(User.Id, token);
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<Error>();
                string ErrorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Error";
                await Js.InvokeVoidAsync("alert", ErrorMessage);
                
            }
           
            await Js.InvokeAsync<object>("console.log", "This is a log message by save button");
            SelectedGenres.Clear();
            _ShowModel = false;
            StateHasChanged();
        }

        protected async Task HandleCloseModal()
        {
        
            SelectedGenres.Clear();
            _ShowModel = false;
            StateHasChanged();
        }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                var customAuthStateProvider = (CustomAuthStateProvider) AuthenticationStateProvider;
                var localStorageUserId = await customAuthStateProvider.GetUserId();
                var token = await customAuthStateProvider.GetToken();

                User = await UserService.GetUser(localStorageUserId, token);

                if(User is null)
                {
                   await customAuthStateProvider.LoggoutUser();
                  
                }
          
            
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }

        }
    }
}
