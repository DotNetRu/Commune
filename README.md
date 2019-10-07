# DotNetRu Server

[![Build status](https://ci.appveyor.com/api/projects/status/k48inxyw1s45avka?svg=true)](https://ci.appveyor.com/project/AnatolyKulakov/server)

DotNetRu Server - продукт семейства DotNetRu, обеспечивающий универсальный API доступа к [Аудиту](https://github.com/DotNetRu/Audit). В связке с [UI](https://github.com/DotNetRu/DevActivator) предполагается предоставление удобного метода добавления и редактирования митапов.

Кроме того, DotNetRu Server является единым API доступа к данным о митапах для всех клиентов (например, [DotNetRu App](https://github.com/DotNetRu/App)).

# Цели

[Аудит](https://github.com/DotNetRu/Audit) является централизованным хранилищем всей информации о прошедших митапах всех сообществ DotNetRu. Данные в Аудите сохранены в формате связанных XML файлов. Их ручное редактирование чревато ошибками и нарушениями формата.

Цель проекта DotNetRu Server - обеспечить REST-like API для редактирования Аудита без необходимости обращаться к GitHub-репозиторию [Аудита](https://github.com/DotNetRu/Audit). В процессе редактирования должна поддерживаться целостность данных и соответствие [форматам хранения](https://github.com/DotNetRu/Audit/tree/master/schemas).

Последняя версия DotNetRu Server всегда развернута на [общедоступном веб-сервере](https://server-dotnetru.azurewebsites.net).

# Связанные проекты

## Аудит

Единое хранилище информации о митапах, площадках, спикерах и докладах. Реализовано как набор XML файлов в [GitHub репозитории](https://github.com/DotNetRu/Audit).

## WebUI

[Веб-интерфейс](https://github.com/DotNetRu/DevActivator) для удобной работы с DotNetRu Server. 

# Текущее состояние и планы

В настоящее время DotNetRu Server имеет разработанный API, но не до конца проведена интеграция со вспомогательными системами. Также отсутствует связь с хранилищем - GitHub-репозиторием Аудита.

В связи с этим план на ближайший этап состоит в следующем:
 - развернуть UI на production сервере и указать URL в этом файле
 - протестировать совместную работу UI и Server по упрощённой схеме без хранилища
 
Желаемый срок окончания ближайшего этапа - 05 ноября 2019 года.

Упрощённая схема взаимодействия заключается в следующем:
- DotNetRu Server работает на in-memory базе данных
- пользователь импортирует базу митапов на сервер через [DotNetRu.Importer](https://github.com/DotNetRu/Server/tree/master/DotNetRuServer.Importer)
- пользователь добавляет/изменяет информацию о новом митапе с помощью UI развернутого на production сервере
- пользователь экспортирует набор XML обратно на компьютер с помощью [DotNetRu.Exporter](https://github.com/DotNetRu/Server/tree/master/DotNetRuServer.Exporter)
- пользователь вручную создаёт pull request в [репозиторий Аудита](https://github.com/DotNetRu/Audit)


# Как подготовить БД к разработке

# Если у вас MacOS, Linux

1) Установить [docker](https://www.docker.com/products/docker-desktop)
2) Перейдите по [ссылке](https://github.com/settings/tokens) и выпустите токен, кнопка "Generate new token"
3) Запустить скрипт `bash ./buildtools/local/initdb.sh "GITHUB_TOKEN"`

# Если у вас Windows
## База данных
В данный момент в проекте используется `MSSQL Server`. При наличии установленного инстанса, этот шаг можно *пропустить*.

1) Установить [docker](https://www.docker.com/products/docker-desktop)
2) Далее, в коммандной строке нужно выполнить следующую команду
```bash
 docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SuperPuperPassword1234' -p 1433:1433 --name DotNetRuDB -d mcr.microsoft.com/mssql/server:2017-latest
 ```
3) После этого необходимо подключиться к БД, с помощью любого инструмента. Пользовать - `sa`, пароль - `SuperPuperPassword1234`, порт - `1433`. После подключения необходимо создать базу данных `DotNetRu`

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
