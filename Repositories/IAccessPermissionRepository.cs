using AccessControlAPI.Models;

namespace AccessControlAPI.Repositories;

public interface IAccessPermissionRepository
{
    Task<IEnumerable<AccessPermission>> GetAllAsync();
    Task<AccessPermission?> GetByIdAsync(int id);
    Task<AccessPermission> AddAsync(AccessPermission permission);
    Task UpdateAsync(AccessPermission permission);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    
    Task<bool> ExistsForEmployeeAndResourceAsync(int employeeId, int resourceId);
    Task<IEnumerable<AccessPermission>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<AccessPermission>> GetByResourceIdAsync(int resourceId);
}