using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Books
{
    public static class BookEndpoints
    {
        public static void MapBookEndpoints(this WebApplication app)
        {
            app.MapGet("/books", (IBookService service) => service.GetBooksAsync());
        }
    }

    internal interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync();
    }

    public record BookDto(Guid Id, string Title, string Author);

    internal class BookService : IBookService
    {
        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            await Task.Delay(100);

            return new[]
            {
                new BookDto(Guid.NewGuid(), "1984", "George Orwell"),
                new BookDto(Guid.NewGuid(), "Brave New World", "Aldous Huxley"),
                new BookDto(Guid.NewGuid(), "Fahrenheit 451", "Ray Bradbury")
            };
        }
    }

    public static class BookServiceExtensions
    {
        public static IServiceCollection AddBookServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}
