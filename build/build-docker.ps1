# Prevent windows container build
if($isWindows){
    exit 0
}

Write-Host Starting build docker
$Version=$env:APPVEYOR_BUILD_VERSION

docker build -t dotnetru/server:latest -t dotnetru/server:$Version .