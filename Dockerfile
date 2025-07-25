# STAGE 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore
COPY . .
WORKDIR /src/api/server
RUN dotnet clean
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# STAGE 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NexKoala.WebApi.Host.dll"]
