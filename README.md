# Dometrain Modular Monoltiths Solution

## Run Tests
```powershell
docker-compose up -d sql
dotnet test
```
Entity Framework migrations are run by the tests, so no need to run it separately.

## Entity Framework Migration
```powershell
dotnet ef migrations add Initial -c BookDbContext -p ..\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web.csproj -o .\Data\Migrations
```

## Build Docker Image
```powershell
docker build -t dometrain-modular-monolith/api -f web.Dockerfile .
```

## Run Web API Container
```powershell
docker-compose build
docker-compose up -d
```
The above command starts the SQL Server, runs the Entity Framework migration that updates the database, and finally, the API.

### Configure Database Connection String
```powershell
dotnet user-secrets init --project .\RiverBooks.Web\RiverBooks.Web.csproj
dotnet user-secrets set "ConnectionStrings:BooksConnectionString" "Server=localhost,2433;Database=RiverBooks_Dev;User Id=sa;Password=gsjTuSW=fVR!a5Tv;TrustServerCertificate=true" --project .\RiverBooks.Web\RiverBooks.Web.csproj
```
### Run Entity Framework Core Migrations
```powershell
dotnet ef database update -c BookDbContext -p RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web\RiverBooks.Web.csproj
```

```powershell
docker run -d -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__BooksConnectionString="Server=dometrain-modular-monoliths-sql-1,2433;Database=RiverBooks_Prod;User Id=sa;Password=gsjTuSW=fVR!a5Tv;TrustServerCertificate=true" --name api dometrain-modular-monolith/api
```