using System.ComponentModel.DataAnnotations;

namespace AccessControlAPI.Models;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(100, ErrorMessage = "Имя не должно превышать 100 символов")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(100, ErrorMessage = "Фамилия не должна превышать 100 символов")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Должность обязательна")]
    public string Position { get; set; } = string.Empty;

    [Required(ErrorMessage = "Отдел обязателен")]
    public string Department { get; set; } = string.Empty;

    [Required(ErrorMessage = "Дата приема обязательна")]
    public DateTime HireDate { get; set; }

    public bool IsActive { get; set; }
}