Write-Host Starting deploy docker image

$env:DOCKER_PASS | docker login --username dotnetrucd --password-stdin

Write-Host Push image to docker hub

docker push dotnetru/server