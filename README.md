# Dometrain Modular Monoltiths Solution

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
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development --name api dometrain-modular-monolith/api
```