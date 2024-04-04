namespace RiverBooks.Books;

internal class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<IEnumerable<BookDto>> ListBooks()
    {
        var books = await bookRepository.List();

        return books.Select(b => new BookDto(b.Id, b.Title, b.Author, b.Price));
    }

    public async Task<BookDto?> GetBookById(Guid id)
    {
        var book = await bookRepository.GetById(id);

        return book is null ? default : new BookDto(book.Id, book.Title, book.Author, book.Price);
    }

    public async Task CreateBook(BookDto newBook)
    {
        var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);
        await bookRepository.Add(book);
        await bookRepository.SaveChanges();
    }

    public async Task DeleteBook(Guid id)
    {
        var bookToDelete = await bookRepository.GetById(id);

        if (bookToDelete is null)
        {
            return;
        }

        await bookRepository.Delete(bookToDelete);
        await bookRepository.SaveChanges();
    }

    public async Task UpdateBookPrice(Guid bookId, decimal newPrice)
    {
        var book = await bookRepository.GetById(bookId);

        // #TODO: Handle case where book does not exist.
        book!.UpdatePrice(newPrice);
        await bookRepository.SaveChanges();
    }
}
