## Build stage
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /build

# Restore dependencies
COPY src/ServiceHost/ServiceHost.csproj ./src/ServiceHost/
COPY test/ServiceHost.Tests/ServiceHost.Tests.csproj ./test/ServiceHost.Tests/

RUN dotnet restore src/ServiceHost/ServiceHost.csproj
RUN dotnet restore test/ServiceHost.Tests/ServiceHost.Tests.csproj

# Copy source
COPY . .

# Run tests
RUN dotnet test test/ServiceHost.Tests/ServiceHost.Tests.csproj

# Publish stage
RUN dotnet publish src/ServiceHost/ServiceHost.csproj -o /publish


## Runtime stage
FROM microsoft/aspnetcore:2.0
COPY --from=build-env /publish /app

WORKDIR /app
ENTRYPOINT ["dotnet", "DotNetRu.ServiceHost.dll"]