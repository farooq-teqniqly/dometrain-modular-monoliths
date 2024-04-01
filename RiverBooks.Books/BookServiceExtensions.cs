using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("BooksConnectionString");
        services.AddDbContext<BookDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookRepository, EfBookRepository>();

        return services;
    }
}
