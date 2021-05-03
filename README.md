# DotNetRu.Commune
![CI status](https://github.com/zetroot/Commune/actions/workflows/main.yml/badge.svg)
[![codecov](https://codecov.io/gh/zetroot/Commune/branch/master/graph/badge.svg)](https://codecov.io/gh/zetroot/Commune)

DotNetRu Commune - продукт семейства DotNetRu, обеспечивающий кроссплатформенный, serverless доустп к [Аудиту](https://github.com/DotNetRu/Audit) с целью удобного метода добавления и редактирования митапов.

# Цели

[Аудит](https://github.com/DotNetRu/Audit) является централизованным хранилищем всей информации о прошедших митапах всех сообществ DotNetRu. Данные в Аудите сохранены в формате связанных XML файлов. Их ручное редактирование чревато ошибками и нарушениями формата.

Цель проекта DotNetRu Commune - обеспечить возможность редактирования Аудита без необходимости обращаться к GitHub-репозиторию [Аудита](https://github.com/DotNetRu/Audit) прямиком из браузера. В процессе редактирования поддерживается целостность данных и соответствие [форматам хранения](https://github.com/DotNetRu/Audit/tree/master/schemas)

Последняя версия DotNetRu Commune всегда развернута на [общедоступном веб-сервере](https://dotnetru.github.io/Commune).

# Связанные проекты

## Аудит

Единое хранилище информации о митапах, площадках, спикерах и докладах. Реализовано как набор XML файлов в [GitHub репозитории](https://github.com/DotNetRu/Audit).

# Текущее состояние и планы

В настоящее время DotNetRu Commune имеет настроенный CI/CD и реализованные инфраструктурные фичи.

# Дальнейшие планы (обсуждаемо)

- Разработать это прекрасное приложение
- ...
