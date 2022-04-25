﻿# BookShelf

Данный проект представляет из себя онлайн-магазин по продаже книг.
Планируется реализовать:

1. Группировку книг по жанрам, по автору, издательству, году издания,
2. Просмотр книг определенного автора,
3. Личный кабинет с информацией о купленных книгах,
4. Корзину покупок

## Запуск

Приложение упаковано в `docker-compose`. Для запуска достаточно скачать 
исходный код и из корня проекта запустить команду `docker-compose up -d`.
После чего сайт приложения будет доступен по адресу `http://localhost`. 
При необходимости, порт приложения можно поменять, отредактировав файл 
`docker-compose.yml`.

В качестве СУБД для приложения использована `PostgreSQL`, настройки 
стандартные, для подключения использовать следующие данные:

| Тип                  | Значение  |
|----------------------|-----------|
| Порт                 | 5432      |
| Имя пользователя     | book_user |
| Название базы данных | book_db   |
| Пароль               | pass1_    |






