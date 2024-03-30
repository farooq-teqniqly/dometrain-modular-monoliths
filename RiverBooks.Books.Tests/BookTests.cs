using FluentAssertions;

namespace RiverBooks.Books.Tests;
public class BookTests
{
    [Fact]
    public void Cannot_Assign_Negative_Price_To_Book()
    {
        Action act = () => CreateBook(price: -1.99m);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Required input price cannot be negative*");
    }

    [Fact]
    public void Cannot_Assign_Default_Guid_As_Book_Id()
    {
        Action act = () => CreateBook(id: Guid.Empty.ToString());

        act.Should().Throw<ArgumentException>()
            .WithMessage("Parameter [id] is default value for type Guid*");
    }

    [Theory]
    [InlineData(null, "Value cannot be null*")]
    [InlineData("   ", "Required input title was empty*")]
    public void Cannot_Assign_Null_Or_Whitespace_Title(string title, string expectedErrorMessage)
    {
        Action act = () => CreateBook(title: title);

        act.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(null, "Value cannot be null*")]
    [InlineData("   ", "Required input author was empty*")]
    public void Cannot_Assign_Null_Or_Whitespace_Author(string author, string expectedErrorMessage)
    {
        Action act = () => CreateBook(author: author);

        act.Should().Throw<ArgumentException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public void When_Updating_Price_Price_Cannot_Be_Negative()
    {
        var book = CreateBook();

        book.Invoking(b => b.UpdatePrice(-1.99m))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Required input newPrice cannot be negative*");
    }

    private Book CreateBook(
        string id = "86bf108f-1b35-4a78-aad8-0854b9cf1de8",
        string title = "1984",
        string author = "George Orwell",
        decimal price = 9.99m)
    {
        return new Book(
            new Guid(id),
            title,
            author,
            price);
    }
}
