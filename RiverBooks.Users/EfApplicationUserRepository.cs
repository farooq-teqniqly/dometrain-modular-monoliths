using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users;

internal class EfApplicationUserRepository(UserDbContext dbContext) : IApplicationUserRepository
{
    public Task<ApplicationUser?> GetUserWithCartByEmail(string emailAddress)
    {
        return dbContext.ApplicationUsers
            .Include(u => u.CartItems)
            .SingleOrDefaultAsync(u => u.Email == emailAddress);
    }

    public Task SaveChanges()
    {
        return dbContext.SaveChangesAsync();
    }
}
