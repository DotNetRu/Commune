## Build stage
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /build

# Copy source
COPY . .

# Restore dependencies
RUN dotnet restore src/ServiceHost/ServiceHost.csproj /p:SolutionDir=/build /p:SolutionName=DotNetRu.Server
RUN dotnet restore test/ServiceHost.Tests/ServiceHost.Tests.csproj /p:SolutionDir=/build /p:SolutionName=DotNetRu.Server

# Run tests
RUN dotnet test test/ServiceHost.Tests/ServiceHost.Tests.csproj /p:SolutionDir=/build /p:SolutionName=DotNetRu.Server

# Publish stage
RUN dotnet publish src/ServiceHost/ServiceHost.csproj -o /publish /p:SolutionDir=/build /p:SolutionName=DotNetRu.Server

## Runtime stage
FROM microsoft/aspnetcore:2.0
COPY --from=build-env /publish /app

WORKDIR /app
ENTRYPOINT ["dotnet", "DotNetRu.ServiceHost.dll"]