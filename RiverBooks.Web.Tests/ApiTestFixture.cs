using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Books;
using RiverBooks.Users;

namespace RiverBooks.Web.Tests;

public class ApiTestFixture : AppFixture<Program>
{
    protected override void ConfigureApp(IWebHostBuilder a)
    {
        a.UseContentRoot(Directory.GetCurrentDirectory());
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Testing.json")
            .Build();

        services.AddDbContext<BookDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BooksConnectionString"), o => o.MigrationsHistoryTable("EFMigrations", "books")));

        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("UsersConnectionString"), o => o.MigrationsHistoryTable("EFMigrations", "users")));

        ApplyDatabaseMigrations(services);
    }

    private void ApplyDatabaseMigrations(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var bookDbContext = scope.ServiceProvider.GetRequiredService<BookDbContext>();
        var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        bookDbContext.Database.Migrate();
        userDbContext.Database.Migrate();
    }
}
