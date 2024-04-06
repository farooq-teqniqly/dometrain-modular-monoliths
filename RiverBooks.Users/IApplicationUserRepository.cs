namespace RiverBooks.Users;

internal interface IApplicationUserRepository
{
    Task<ApplicationUser?> GetUserWithCartByEmail(string emailAddress);
    Task SaveChanges();
}
