#!/usr/bin/env bash

GITHUB_TOKEN=$1
CONNECTION_STRING="Server=localhost;Database=DotNetRu;User Id=sa;Password=SuperPuperPassword1234;"

echo "Start to work"
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SuperPuperPassword1234' -p 1433:1433 --name DotNetRuDB -d mcr.microsoft.com/mssql/server:2017-latest 
echo "DB server created"
docker exec -it DotNetRuDB /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "SuperPuperPassword1234" -Q "
CREATE DATABASE DotNetRu
GO
"
echo "DB created"
dotnet tool install -g FluentMigrator.DotNet.Cli
echo "Start to build migration csproj"
dotnet build ../../DotNetRuServer.Migrations/DotNetRuServer.Migrations.csproj
echo "Migration tool installed. Start to migrate"
dotnet fm migrate -p sqlserver -c "$CONNECTION_STRING" -a "../../DotNetRuServer.Migrations/bin/Debug/netstandard2.0/DotNetRuServer.Migrations.dll"
echo "Migrations finished successfully"
echo "Start to build an importer"
dotnet build ../../DotNetRuServer.Importer/DotNetRuServer.Importer.csproj
echo "Start an import process"
dotnet ../../DotNetRuServer.Importer/bin/Debug/netcoreapp2.2/DotNetRuServer.Importer.dll "$CONNECTION_STRING" "$GITHUB_TOKEN"
echo "DB is ready for working"