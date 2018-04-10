## Build stage
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /build

# Restore dependencies
COPY src/ServiceHost/ServiceHost.csproj ./src/ServiceHost/
COPY test/Server.Tests/Server.Tests.csproj ./test/Server.Tests/

RUN dotnet restore src/ServiceHost/ServiceHost.csproj
RUN dotnet restore test/Server.Tests/Server.Tests.csproj

# Copy source
COPY . .

# Run tests
RUN dotnet test test/Server.Tests/Server.Tests.csproj

# Publish stage
RUN dotnet publish src/ServiceHost/ServiceHost.csproj -o /publish


## Runtime stage
FROM microsoft/aspnetcore:2.0
COPY --from=build-env /publish /app

WORKDIR /app
ENTRYPOINT ["dotnet", "ServiceHost.dll"]