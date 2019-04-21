# DotNetRu Server

[![Build status](https://ci.appveyor.com/api/projects/status/k48inxyw1s45avka?svg=true)](https://ci.appveyor.com/project/AnatolyKulakov/server)


# Как подготовить БД к разработке

# Если у вас MacOS, Linux
Запустить скрипт `buildtools/local/initdb.sh`

# Если у вас Windows
## База данных
В данный момент в проекте используется `MSSQL Server`. При наличии установленного инстанса, этот шаг можно *пропустить*.

1) Установить [docker](https://www.docker.com/products/docker-desktop)
2) Далее, в коммандной строке нужно выполнить следующую команду
```bash
 docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SuperPuperPassword1234' -p 1433:1433 --name DotNetRuDB -d mcr.microsoft.com/mssql/server:2017-latest
 ```
3) После этого необходимо подключиться к БД, с помощью любого инструмента. Пользовать - `sa`, пароль - `DevPassword`, порт - `1433`. После подключения необходимо создать базу данных `DotNetRu`

## Миграция данных
Для работы с мигратором необходимо выполнить несколько шагов

1) Установить - `dotnet tool install -g FluentMigrator.DotNet.Cli`
2) Сбилдить проект `DotNetRuServer.Migrations`
3) Перейти в папку `bin/Debug/netstandard2.0`
4) Вызвать в командной строке - `dotnet fm migrate -p sqlserver -c "Server=localhost;Database=DotNetRu;User Id=sa;Password=SuperPuperPassword1234;" -a "DotNetRuServer.Migrations.dll"`
5) Радоваться жизни, вы почти справились :)

## Импорт данных с гитхаба
Для работы импортера, ему нужно два параметра - гитхаб-токен и conntections-string к базе данных

1) Перейдите по [ссылке](https://github.com/settings/tokens) и выпустите токен, кнопка "Generate new token"
2) Запустите DotNetRuServer.Importer с передачей первым аргументом `Server=localhost;Database=DotNetRu;User Id=sa;Password=SuperPuperPassword1234;`, а вторым аргументом полученный гитхаб-токен
3) Подождите ~2 минуты
4) У вас есть готовая схема данных, можно творить :)