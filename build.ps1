$Version = $env:APPVEYOR_BUILD_VERSION
Write-Host Starting build $Version $env:APPVEYOR_BUILD_WORKER_IMAGE

If($isWindows)
{
    dotnet build ./src/ServiceHost/ServiceHost.csproj --configuration Release
    
    Write-Host Publish windows
    
    # windows
    dotnet publish ./src/ServiceHost/ServiceHost.csproj --runtime win10-x64 --configuration Release --no-restore --output ./../../artifacts-windows

    Write-Host Publish osx
    
    # osx
    dotnet publish ./src/ServiceHost/ServiceHost.csproj --runtime osx-x64 --configuration Release --no-restore --output ./../../artifacts-osx
}
Else
{
    dotnet build ./src/ServiceHost/ServiceHost.csproj --runtime ubuntu-x64 --configuration Release
    
    Write-Host Publish linux
    
    # linux
    dotnet publish ./src/ServiceHost/ServiceHost.csproj --runtime ubuntu-x64 --configuration Release --no-restore --output ./../../artifacts-linux
}