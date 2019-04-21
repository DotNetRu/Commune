## Build stage
FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /build

# Copy source
COPY . .

# Restore dependencies
RUN dotnet restore DotNetRuServer/DotNetRuServer.csproj

# Publish stage
RUN dotnet publish DotNetRuServer/DotNetRuServer.csproj -o /publish /p:SolutionDir=/build

## Runtime stage
FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=build-env /publish /app

WORKDIR /app
ENTRYPOINT ["dotnet", "DotNetRuServer.dll"]