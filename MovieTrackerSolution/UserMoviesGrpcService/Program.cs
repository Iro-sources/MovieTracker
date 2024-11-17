using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using UserMoviesGrpcService.Models;
using UserMoviesGrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.Services.AddCors();
// Add services to the container.
builder.Services.AddDbContext<UserMovieDbContext>(options =>
        options
        .UseSqlServer(builder.Configuration.GetConnectionString("UserMovieData")), ServiceLifetime.Transient);
builder.Services.AddGrpc(options => { options.EnableDetailedErrors = true; });

var app = builder.Build();
app.UseCors(policy =>
{
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});
app.UseGrpcWeb();
// Configure the HTTP request pipeline.
app.MapGrpcService<MovieServiceImp>().EnableGrpcWeb();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
