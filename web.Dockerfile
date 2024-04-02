FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY . ./
RUN dotnet build "RiverBooks.Web/RiverBooks.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RiverBooks.Web/RiverBooks.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "RiverBooks.Web.dll"]