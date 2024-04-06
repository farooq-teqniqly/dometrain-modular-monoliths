using System;

public record BookDetailsResponse(Guid BookId, string Author, string Title, string Price);

public record BookDetailsQuery(Guid BookId) : IRequest;
