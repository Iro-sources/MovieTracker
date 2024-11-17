using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace UserManagment.Api.Tests;

public class IntegrationTestFactory<TStartup, TDbContext> : WebApplicationFactory<TStartup>, IAsyncLifetime where TStartup : class where TDbContext : DbContext
{
    private readonly MsSqlContainer _dbContainer;

    public IntegrationTestFactory()
    {
        _dbContainer = new MsSqlBuilder()
                        .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDevDbContext<TDbContext>();
            // Build the DbContextOptions and store it in a local variable
            var dbContextOptions = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(_dbContainer.GetConnectionString())
                .Options;

            // Register the DbContextOptions as Singleton
            services.AddSingleton(dbContextOptions);

            services.AddDbContext<TDbContext>(options => options.UseSqlServer(_dbContainer.GetConnectionString()));
            services.EnsureDbCreated<TDbContext>();
        });
    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }
    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}




public class Content<T>
{
    public T? Data { get; set; }

    public Content(T data)
    {
        Data = data;
    }
}
public class Error
{
    public bool Status { get; set; } = false;
    public string ErrorMessage { get; set; }
    public string? ErrorDetails { get; set; }
}
