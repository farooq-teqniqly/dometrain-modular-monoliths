using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books;

internal class BookConfiguration: IEntityTypeConfiguration<Book>
{
    private const string Author = "Cormac McCarthy";
    
    internal static readonly List<Book> SeedBooks =
    [
        new(new("ec5785b5-ae50-4be4-8f58-35190fcbed9f"), "Blood Meridian", Author, 9.99m),
        new(new("7f9680cf-9130-41c4-8b88-252de10df631"), "No Country for Old Men", Author, 12.99m),
        new(new("4551182a-5ad4-4d10-9276-ec055719d9f3"), "The Road", Author, 14.99m)
    ];

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(p => p.Title).HasMaxLength(DataSchemaConstants.DefaultNameLength).IsRequired();
        builder.Property(p => p.Author).HasMaxLength(DataSchemaConstants.DefaultNameLength).IsRequired();
        builder.HasData(GetSampleBookData());
    }

    private static List<Book> GetSampleBookData() => SeedBooks;
}
