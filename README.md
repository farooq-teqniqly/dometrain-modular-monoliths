# Dometrain Modular Monoltiths Solution

## Entity Framework Migration
```powershell
dotnet ef migrations add Initial -c BookDbContext -p ..\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web.csproj -o .\Data\Migrations
```