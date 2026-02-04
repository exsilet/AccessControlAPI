using System.ComponentModel.DataAnnotations;

namespace AccessControlAPI.Models;

public class Resource
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название ресурса обязательно")]
    [StringLength(200, ErrorMessage = "Название не должно превышать 200 символов")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Тип ресурса обязателен")]
    public ResourceType Type { get; set; }

    [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Некорректный формат IP-адреса")]
    public string? IpAddress { get; set; }

    public bool IsActive { get; set; } = true;
}