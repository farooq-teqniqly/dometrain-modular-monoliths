namespace RiverBooks.Books;

internal interface IBookService
{
    Task<IEnumerable<BookDto>> ListBooks();
    Task<BookDto> GetBookById(Guid id);
    Task CreateBook(BookDto newBook);
    Task DeleteBook(Guid id);
    Task UpdateBookPrice(Guid bookId, decimal newPrice);
}
