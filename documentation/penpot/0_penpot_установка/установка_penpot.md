# Установка Penpot локально в Docker

Для установки Penpot локально в Docker необходимо, чтобы выполнялись следующие условия:

- Установлен Docker Engine. Проверить можно командой:

  ```
  docker --version
  ```

  Если Docker не установлен, ознакомиться с официальной документацией можно по ссылке: [установка Docker](https://docs.docker.com/get-docker/).

- Рекомендуется установить Docker Compose (для удобства). Проверить наличие можно командой:
  ```
  docker compose
  ```
  Инструкция по установке Docker Compose: [установка Docker Compose](https://docs.docker.com/compose/install/).

_Запуск Penpot для проектирования интерфейсов_

Запуск Penpot в Docker осуществляется с помощью команды:

```bash
docker compose up -p <название_например_penpot> -f <путь_к_файлу-compose.yml> -d
```

Команду нужно выполнить в терминале, находясь в директории, где расположен файл `*-compose.yml` (например `docker-compose.yml` или `penpot-compose.yml`), либо указать полный путь к этому файлу.

После выполнения команды Penpot будет запущен в Docker и доступен по адресу `http://localhost:9001` (или по другому адресу/порту, если вы указали другой порт в файле `*-compose.yml`).
