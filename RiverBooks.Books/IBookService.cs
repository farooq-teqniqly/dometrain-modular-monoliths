namespace RiverBooks.Books;

internal interface IBookService
{
    Task<IEnumerable<BookDto>> GetBooksAsync();
}
