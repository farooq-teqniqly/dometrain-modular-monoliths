# Dometrain Modular Monoltiths Solution

## Run Tests
```powershell
docker-compose up -d sql
dotnet test
```
Entity Framework migrations are run by the tests, so no need to run it separately.

## Configure Web API for HTTPS
```powershell
dotnet dev-certs https --clean
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx"  -p Xr4iR6dM_+iswtMD
dotnet dev-certs https --trust
```

## Run Web API Container
```powershell
docker-compose build
docker-compose up
```
The above command starts the SQL Server, runs the Entity Framework migration that updates the database, and finally, the API. Browse to `http://localhost:8080/swagger` to view the Swagger page.

## Entity Framework Migration
```powershell
dotnet ef migrations add InitialBooks -c BookDbContext -p .\RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web\RiverBooks.Web.csproj -o .\Data\Migrations
dotnet ef migrations add InitialUsers -c UserDbContext -p .\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web\RiverBooks.Web.csproj -o .\Data\Migrations
dotnet ef database update -c BookDbContext -p RiverBooks.Books\RiverBooks.Books.csproj -s .\RiverBooks.Web\RiverBooks.Web.csproj
dotnet ef database update -c UserDbContext -p RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web\RiverBooks.Web.csproj
```

## Build Docker Image
```powershell
docker build -t dometrain-modular-monolith/api -f web.Dockerfile .
```


### Configure Database Connection String
```powershell
dotnet user-secrets init --project .\RiverBooks.Web\RiverBooks.Web.csproj
dotnet user-secrets set "ConnectionStrings:BooksConnectionString" "Server=localhost,2433;Database=RiverBooks_Dev;User Id=sa;Password=gsjTuSW=fVR!a5Tv;TrustServerCertificate=true" --project .\RiverBooks.Web\RiverBooks.Web.csproj
dotnet user-secrets set "ConnectionStrings:UsersConnectionString" "Server=localhost,2433;Database=RiverBooks_Dev;User Id=sa;Password=gsjTuSW=fVR!a5Tv;TrustServerCertificate=true" --project .\RiverBooks.Web\RiverBooks.Web.csproj
dotnet user-secrets set "Auth:JwtSecret" "135604c464fb19a0d2f18a6d6bf4a8aec67fc3246822a2491a5fda51870c874341b4aaa25f16195c5c00ef27be5429756210f0e5adecc06900ac949bf4f096e2dacd4827ad5171d5e4d0a265e294296b84df14b7e1e774fc16cca6f706a8e1d741fd7e038f571d2282b3425dd8a958675104d9ae24601224400b83cab8a909fc5ecd663f493d67ee5b88e5b60df7afdd7a14625fda322b7e12af95e551774a1c07f8c0f9df7bee246f3dff40a2e8c6161ec55b63486fa500d6c06551a4ffc6aba39d4e221063ef11c27d131374f28f466a2a6830df72324d8a5428721dacfb6a7e79916c52031ba4c5e065e84318e9d1c2b0f3a74bd0ccb210da4e4f7fdd0540"
```

```powershell
docker run -d -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__BooksConnectionString="Server=dometrain-modular-monoliths-sql-1,2433;Database=RiverBooks_Prod;User Id=sa;Password=gsjTuSW=fVR!a5Tv;TrustServerCertificate=true" -e ConnectionStrings__UsersConnectionString="Server=dometrain-modular-monoliths-sql-1,2433;Database=RiverBooks_Prod;User Id=sa;Password=gsjTuSW=fVR!a5Tv;TrustServerCertificate=true" --name api dometrain-modular-monolith/api
```