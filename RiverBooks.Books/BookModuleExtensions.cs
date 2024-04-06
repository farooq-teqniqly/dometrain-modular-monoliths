using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RiverBooks.Books;

public static class BookModuleExtensions
{
    public static IServiceCollection AddBookServices(
        this IServiceCollection services,
        ConfigurationManager configuration, 
        ILogger logger, 
        List<Assembly> mediatRAssemblies)
    {
        var connectionString = configuration.GetConnectionString("BooksConnectionString");
        services.AddDbContext<BookDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        });

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookRepository, EfBookRepository>();

        mediatRAssemblies.Add(typeof(BookModuleExtensions).Assembly);

        logger.Information("{Module} module services registered.", "Books");

        return services;
    }
}
