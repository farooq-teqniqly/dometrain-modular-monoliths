using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books;

internal class BookConfiguration: IEntityTypeConfiguration<Book>
{
    internal static readonly Guid BookId1 = new("ec5785b5-ae50-4be4-8f58-35190fcbed9f");
    internal static readonly Guid BookId2 = new("7f9680cf-9130-41c4-8b88-252de10df631");
    internal static readonly Guid BookId3 = new("4551182a-5ad4-4d10-9276-ec055719d9f3");

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(p => p.Title).HasMaxLength(DataSchemaConstants.DefaultNameLength).IsRequired();
        builder.Property(p => p.Author).HasMaxLength(DataSchemaConstants.DefaultNameLength).IsRequired();
        builder.HasData(GetSampleBookData());
    }

    private IEnumerable<Book> GetSampleBookData()
    {
        var author = "Cormac McCarthy";

        yield return new Book(BookId1, "Blood Meridian", author, 9.99m);
        yield return new Book(BookId2, "No Country for Old Men", author, 12.99m);
        yield return new Book(BookId3, "The Road", author, 14.99m);
    }
}