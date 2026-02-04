using System.ComponentModel.DataAnnotations;

namespace AccessControlAPI.Models;

public class AccessPermission
{
    public AccessPermission()
    {
        GrantedDate = DateTime.UtcNow;
    }
    
    public int Id { get; set; }
    
    [Required(ErrorMessage = "EmployeeId обязателен")]
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный EmployeeId")]
    public int EmployeeId { get; set; }
    
    [Required(ErrorMessage = "ResourceId обязателен")]
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ResourceId")]
    public int ResourceId { get; set; }
    
    [Required(ErrorMessage = "Уровень доступа обязателен")]
    public AccessLevel AccessLevel { get; set; }
    
    public DateTime GrantedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    public bool IsActive { get; set; }
}