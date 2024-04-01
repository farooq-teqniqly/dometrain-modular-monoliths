using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyBookRepository
{
    Task Add(Book book);
    Task Delete(Book book);
    Task SaveChanges();

}
internal class EfBookRepository : IBookRepository
{
    private readonly BookDbContext _dbContext;

    public EfBookRepository(BookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book?> GetById(Guid id)
    {
        return await _dbContext.Books.FindAsync(id);
    }

    public async Task<List<Book>> List()
    {
        return await _dbContext.Books.ToListAsync();
    }

    public Task Add(Book book)
    {
        _dbContext.Add(book);
        return Task.CompletedTask;
    }

    public Task Delete(Book book)
    {
        _dbContext.Remove(book);
        return Task.CompletedTask;
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}
