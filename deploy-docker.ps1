# Prevent windows container build
if($isWindows){
    exit 0
}

Write-Host Starting deploy docker

docker images

Write-Host Docker login

$env:DOCKER_PASS | docker login --username dotnetrucd --password-stdin

Write-Host Push to docker hub

docker push dotnetru/server