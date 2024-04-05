namespace RiverBooks.Users;

public record CreateUserRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);
