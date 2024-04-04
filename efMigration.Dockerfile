FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app
COPY . ./

RUN chmod +x ef-migration.sh

ENTRYPOINT ["./ef-migration.sh"]