#!/bin/bash

echo "Installing dotnet-ef tool..."
dotnet tool install --global dotnet-ef

if [ $? -eq 0 ]; then
    echo "dotnet-ef tool installed successfully."
else
    echo "Failed to install dotnet-ef tool. Exiting..."
    exit 1
fi

echo "Adding dotnet tools directory to PATH..."
export PATH="$PATH:/root/.dotnet/tools"

echo "Verifying PATH..."
echo $PATH

echo "Setup completed successfully."

echo "Running EF Core migration..."
dotnet ef database update -c BookDbContext -p RiverBooks.Books/RiverBooks.Books.csproj -s RiverBooks.Web/RiverBooks.Web.csproj
dotnet ef database update -c UserDbContext -p RiverBooks.Users/RiverBooks.Users.csproj -s RiverBooks.Web/RiverBooks.Web.csproj

if [ $? -eq 0 ]; then
    echo "Database updated."
else
    echo "Failed to update database."
    exit 1
fi