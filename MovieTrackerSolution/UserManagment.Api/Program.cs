using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UserManagment.Api.Data;
using UserManagment.Api.Repositoies;
using UserManagment.Api.Repositoies.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// pool of database contexts (pool size is set to 512 by default) reusing existing context insted of creating new once for each request.
builder.Services.AddDbContextPool<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserManagment"));
});


// injecting userRepositoy to each class that deppends on it. (by each request)
builder.Services.AddScoped<IUserRepository, UserRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }