namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyBookRepository
{
    Task Add(Book book);
    Task Delete(Book book);
    Task SaveChanges();

}

internal class BookRepository : IBookRepository
{
    public Task<Book?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book>> List()
    {
        return Task.FromResult(new List<Book>()
        {
            new(Guid.NewGuid(), "1984", "George Orwell", 9.99m),
            new(Guid.NewGuid(), "Brave New World", "Aldous Huxley", 5.99m),
            new(Guid.NewGuid(), "Fahrenheit 451", "Ray Bradbury", 8.99m)
        });
    }

    public Task Add(Book book)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Book book)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}
