# Access Control API

Система управления доступом сотрудников к ресурсам компании на ASP.NET Core 8.

## 📋 Функционал
- Управление сотрудниками (CRUD)
- Управление ресурсами (серверы, БД, приложения)
- Назначение прав доступа (Read/Write/Admin)
- Проверка валидации данных
- Поиск разрешений по сотруднику/ресурсу

## 🛠 Технологии
- **ASP.NET Core 8** - Web API
- **Entity Framework Core** - ORM
- **PostgreSQL** - база данных
- **Repository Pattern** - архитектура
- **Swagger/OpenAPI** - документация
- **Git** - контроль версий

## 🚀 Быстрый старт

### Предварительные требования
- .NET 8 SDK
- PostgreSQL 14+
- Git

### Установка
```bash
# Клонирование репозитория
git clone https://github.com/exsilet/AccessControlAPI.git
cd AccessControlAPI

# Восстановление зависимостей
dotnet restore

# Настройка базы данных
# 1. Создайте БД AccessControlDB в PostgreSQL
# 2. Настройте строку подключения в appsettings.json

# Применение миграций
dotnet ef database update

# Запуск приложения
dotnet run
```
## API Endpoints

| Метод | Endpoint | Описание |
|-------|----------|----------|
| GET | `/api/employees` | Список сотрудников |
| POST | `/api/employees` | Создать сотрудника |
| GET | `/api/employees/{id}` | Получить сотрудника |
| GET | `/api/resources` | Список ресурсов |
| POST | `/api/resources` | Создать ресурс |
| GET | `/api/accesspermissions` | Все разрешения |
| POST | `/api/accesspermissions` | Создать разрешение |

## Примеры запросов

```json
// Создание сотрудника
POST /api/employees
{
  "firstName": "Иван",
  "lastName": "Иванов",
  "email": "ivan@company.com",
  "position": "Разработчик",
  "department": "IT",
  "hireDate": "2024-01-15T10:00:00Z",
  "isActive": true
}

// Создание разрешения
POST /api/accesspermissions
{
  "employeeId": 1,
  "resourceId": 1,
  "accessLevel": 2,
  "expiryDate": "2025-12-31T23:59:59Z",
  "isActive": true
}
```
## 📁 Структура проекта
AccessControlAPI/

├── Controllers/ # API контроллеры

├── Models/ # Сущности БД

├── Repositories/ # Слой доступа к данным

├── Migrations/ # Миграции EF Core

├── Program.cs # Точка входа

└── appsettings.json # Конфигурация


## 📝 Лицензия

MIT
