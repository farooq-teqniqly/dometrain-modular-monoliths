namespace RiverBooks.Books;

internal class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<IEnumerable<BookDto>> GetBooksAsync()
    {
        var books = await _bookRepository.List();

        return books.Select(b => new BookDto(b.Id, b.Title, b.Author, b.Price));
    }
}
