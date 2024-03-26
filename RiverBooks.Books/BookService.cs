namespace RiverBooks.Books;

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