# CTMS
*PS. От файла README.md до последней строчки кода было реализовано [Маратом. Х](https://t.me/blanxxty) без использованя ИИ*
<img src="https://github.com/BlanxxtyIS/CTMS/blob/main/scale_1200.jpg" width="800">
## Corporate Task Management System

Это система управления задачами для компаний, похожая на Jira или Trello, но в традиционном, простом исполнении.  
Она позволяет командам эффективно управлять проектами и задачами.  
## Основные возможности системы: ##
### 1. Управление пользователями:  ###
- Регистрация и вход в систему  
- Разные роли пользователей (Admin, Project Manager, Developer, Tester)  
- Профили пользователей с информацией об их активности
### 2. Управление проектами:  ###
- Создание новых проектов  
- Добавление участников в проект  
- Отслеживание прогресса проекта  
- Установка сроков выполнения
### 3. Управление задачами: ###
- Создание задач внутри проектов  
- Назначение задач исполнителям  
- Установка приоритетов и сроков  
- Отслеживание статуса выполнения  
- Комментирование задач  
- Прикрепление файлов
     
## Технические особенности: ##
### 1. Безопасность: ###  
   - JWT токены для безопасной аутентификации  
   - Хранение паролей в хешированном виде  
   - Система ролей и разрешений
-----

### 2. База данных: ###
   - PostgreSQL для хранения данных  
   - Эффективная структура с связями между таблицами  
   - Миграции для управления схемой базы данных
-----

### 3. API: ###
- RESTful API для всех операций
- Swagger документация
- Валидация данных
-----
  
### 4. Дополнительно: ###
- еширование реферш токена, сохранение настроек пользователя
-  Логирование действий  
- Обработка ошибок  
- Пагинация результатов  
- Фильтрация и поиск
-----

### 5. Роли пользователей и их возможности: ###
#### 5. 1 Administrator:  ####
- Управление пользователями  
- Создание и удаление проектов  
- Полный доступ ко всем функциям  
#### 5. 2 Project Manager:  ####
- Управление проектами  
- Создание и назначение задач  
- Добавление участников в проект  
#### 5. 3 Developer:  ####
- Работа с назначенными задачами  
- Обновление статуса задач  
- Комментирование  
#### 5. 4 Tester:  ####
- Тестирование задач  
- Создание баг-репортов  
- Изменение статуса задач  
#### 5. 5 Структура базы данных:  ####
- Users (Пользователи)  
- Projects (Проекты)  
- Tasks (Задачи) 
- Comments (Комментарии)  
- TaskAttachments (Прикрепленные файлы)   
- ProjectUsers (Связь между проектами и пользователями)  


# Реализованный фунционал, приоберетенные навыки #
|Навык/Технология|Затрачено часов|Усовение|
|----------------|---------------|--------|
|Markdown и LaTex| 2 часа        | 80 %   |
|Git и GitHub    | 8 часов       | 70 %   |
|HTML            | 10 часов      | 60 %   |
|Clean Archittecture| 2 часа     | 90 %   |
|Authentication and Authorization| 2часа |90%|
|JWT-Tokens | 3 часа | 80 %|
