## Build stage
FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /build

# Copy source
COPY . .

# Restore dependencies
RUN dotnet restore DevActivator/DevActivator.csproj

# Publish stage
RUN dotnet publish DevActivator/DevActivator.csproj -o /publish /p:SolutionDir=/build

## Runtime stage
FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=build-env /publish /app

WORKDIR /app
ENTRYPOINT ["dotnet", "DevActivator.dll"]