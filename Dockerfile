## Build stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /build

# Copy source
COPY . .

# Restore dependencies
RUN dotnet restore DotNetRuServer/DotNetRuServer.csproj

# Publish stage
RUN dotnet publish DotNetRuServer/DotNetRuServer.csproj -o /publish /p:SolutionDir=/build

## Runtime stage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# Environment Variables
ENV ASPNETCORE_ENVIRONMENT Development

COPY --from=build-env /publish /app

WORKDIR /app
ENTRYPOINT ["dotnet", "DotNetRuServer.dll"]
