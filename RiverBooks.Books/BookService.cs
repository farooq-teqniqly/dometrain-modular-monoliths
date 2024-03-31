namespace RiverBooks.Books;

internal class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<IEnumerable<BookDto>> ListBooks()
    {
        var books = await _bookRepository.List();

        return books.Select(b => new BookDto(b.Id, b.Title, b.Author, b.Price));
    }

    public async Task<BookDto> GetBookById(Guid id)
    {
        var book = await _bookRepository.GetById(id);

        // #TODO: Handle case where book does not exist.

        return new BookDto(book!.Id, book.Title, book.Author, book.Price);
    }

    public async Task CreateBook(BookDto newBook)
    {
        var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);
        await _bookRepository.Add(book);
        await _bookRepository.SaveChanges();
    }

    public async Task DeleteBook(Guid id)
    {
        var bookToDelete = await _bookRepository.GetById(id);

        if (bookToDelete is null)
        {
            return;
        }

        await _bookRepository.Delete(bookToDelete);
        await _bookRepository.SaveChanges();
    }

    public async Task UpdateBookPrice(Guid bookId, decimal newPrice)
    {
        var book = await _bookRepository.GetById(bookId);

        // #TODO: Handle case where book does not exist.
        book!.UpdatePrice(newPrice);
        await _bookRepository.SaveChanges();
    }
}
