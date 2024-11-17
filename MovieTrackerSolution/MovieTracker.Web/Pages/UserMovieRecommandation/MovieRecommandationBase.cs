using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MovieTracker.Models.Dtos.MovieRecommendation.Dtos;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Web.Services;
using MovieTracker.Web.Services.Contracts;
using MovieTracker.Web.states.auth;
using System;
using System.Collections.Generic;

namespace MovieTracker.Web.Pages.UserMovieRecommandation
{
    public class MovieRecommandationBase : ComponentBase
    {

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }


        [Inject]
        private IJSRuntime Js { get; set; }


        [Inject]
        private IMovieDataService MovieDataService { get; set; }



        [Inject]
        private IUserService UserService { get; set; }


        [Inject]
        private NavigationManager NavManager { get; set; }

        public string ErrorMessage { get; set; }


        public UserReadDto User { get; set; }


        public IEnumerable<AdvanceSearchMovieDto> Movies { get; set; }



        [Parameter]
        public string SelectedOption { get; set; }



        public List<string> UserGenres { get; set; }





        public void ShowMovieDetails(string movieId)
        {
            NavManager.NavigateTo($"/movies/{movieId}");
        }


        public async void OnSelectedOptionChange(ChangeEventArgs e)
        {
            SelectedOption = e.Value.ToString();
            await Js.InvokeVoidAsync("console.log", SelectedOption);
            List<string> selectedGenre = new List<string> { SelectedOption };
            await LoadData(selectedGenre);
            StateHasChanged();
        }


        protected override async Task OnParametersSetAsync()
        {
            if(SelectedOption is not null)
            {
                await Js.InvokeVoidAsync("console.log", SelectedOption);
                List<string> selectedGenre = new List<string>
                {
                    SelectedOption
                };

                await LoadData(selectedGenre);
                return;
            }
            return;
        }

        protected override async Task OnInitializedAsync()
        {
            var customAuthStateProvider = (CustomAuthStateProvider)AuthenticationStateProvider;
            var localStorageUserId = await customAuthStateProvider.GetUserId();
            var token = await customAuthStateProvider.GetToken();
            User = await UserService.GetUser(localStorageUserId, token);
            UserGenres = User.SubscribedGenres;
            if(UserGenres.Any())
            {
                if(UserGenres.Count >= 3)
                {
                    var random = new Random();
                    UserGenres = UserGenres.OrderBy(x => random.Next()).ToList();
                    var selectedGenres = UserGenres.Take(random.Next(1, 3)).ToList();

                    await LoadData(selectedGenres);
                }
                else
                {
                    await LoadData(UserGenres);
                }
               
            }
            else
            {
                return;
            }
         
        }


        private async Task LoadData(List<string> selectedGenre)
        {
            try
            {
           
       
                await Js.InvokeVoidAsync("console.log", selectedGenre);

                IEnumerable<AdvanceSearchMovieDto> movieData = await MovieDataService.MovieRecommendation(selectedGenre);
                Movies = movieData;
                await Js.InvokeVoidAsync("console.log", Movies);
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }



}




