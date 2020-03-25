FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Microservice.CartManager/*.csproj ./Microservice.CartManager/
RUN dotnet restore

# Copy everything else and build
COPY Microservice.CartManager/. ./Microservice.CartManager/
WORKDIR /source/Microservice.CartManager
RUN dotnet publish -c release -o /app --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Microservice.CartManager.dll"]