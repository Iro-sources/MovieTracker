using Blazored.LocalStorage;
using UserMoviesGrpcService;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieTracker.Web;
using MovieTracker.Web.Services;
using MovieTracker.Web.Services.Contracts;
using MovieTracker.Web.states.auth;
using MudBlazor.Services;
using Grpc.Net.Client.Web;
using static System.Net.WebRequestMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieDataService, MovieDataService>();
builder.Services.AddScoped<IUserMoviesService, UserMoviesService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();

builder.Services.AddScoped(services =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    //var baseUri = builder.HostEnvironment.BaseAddress;
    var baseUri = "https://localhost:7264";
    var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });
    return new MovieService.MovieServiceClient(channel);
});


await builder.Build().RunAsync();


