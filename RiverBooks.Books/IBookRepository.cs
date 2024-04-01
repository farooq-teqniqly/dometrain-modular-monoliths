using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyBookRepository
{
    Task Add(Book book);
    Task Delete(Book book);
    Task SaveChanges();

}
internal class EfBookRepository(BookDbContext dbContext) : IBookRepository
{
    public async Task<Book?> GetById(Guid id)
    {
        return await dbContext.Books.FindAsync(id);
    }

    public async Task<List<Book>> List()
    {
        return await dbContext.Books.ToListAsync();
    }

    public Task Add(Book book)
    {
        dbContext.Add(book);
        return Task.CompletedTask;
    }

    public Task Delete(Book book)
    {
        dbContext.Remove(book);
        return Task.CompletedTask;
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
