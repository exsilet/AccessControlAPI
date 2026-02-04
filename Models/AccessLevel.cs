namespace AccessControlAPI.Models;

public enum AccessLevel
{
    None = 0,      // Отсутствие доступа
    Read = 1,      // Только чтение
    Write = 2,     // Чтение и запись
    Admin = 3,     // Полный доступ
    Custom = 4     //Спец доступ 
}