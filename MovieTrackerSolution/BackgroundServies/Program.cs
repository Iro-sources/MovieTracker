using BackgroundServies;
using BackgroundServies.Api.configurations;
using BackgroundServies.Api.Service;
using BackgroundServies.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiKeyConfiguration>(builder.Configuration.GetSection("ApiKeys"));

// DI for our controllers and RestClientService
builder.Services.AddSingleton<IRestClientService, RestClientService>();

// Register the ComingSoonController as a hosted service
builder.Services.AddHostedService<MovieUpdateService>();
builder.Services.AddDbContext<ComingSoonDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Singleton); // Change the service lifetime to Singleton




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.AllowAnyOrigin().AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
