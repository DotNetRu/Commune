invoke-expression '$Env:ASPNETCORE_ENVIRONMENT = "Development"'
$webpack = Start-Process -FilePath "powershell" -ArgumentList "node node_modules/webpack/bin/webpack.js --watch" -PassThru -NoNewWindow
$dotnet = Start-Process -FilePath "powershell" -ArgumentList "dotnet electronize start" -PassThru -NoNewWindow