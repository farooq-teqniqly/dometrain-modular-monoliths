namespace RiverBooks.Users;

public record LoginResponse(string Email, string Token);

public record ListCartItemsResponse(List<CartItemDto> CartItems);
