using AccessControlAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessControlAPI.Repositories;

public class AccessPermissionRepository : IAccessPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public AccessPermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccessPermission>> GetAllAsync()
    {
        return await _context.AccessPermissions.ToListAsync();
    }

    public async Task<AccessPermission?> GetByIdAsync(int id)
    {
        return await _context.AccessPermissions.FindAsync(id);
    }

    public async Task<AccessPermission> AddAsync(AccessPermission permission)
    {
        var employeeExists = await _context.Employees.AnyAsync(e => e.Id == permission.EmployeeId);
        if (!employeeExists)
            throw new ArgumentException($"Employee с ID {permission.EmployeeId} не существует");
        
        var resourceExists = await _context.Resources.AnyAsync(r => r.Id == permission.ResourceId);
        if (!resourceExists)
            throw new ArgumentException($"Resource с ID {permission.ResourceId} не существует");
        
        var duplicateExists = await _context.AccessPermissions.AnyAsync(ap => ap.EmployeeId == permission.EmployeeId && ap.ResourceId == permission.ResourceId);
        if (duplicateExists)
            throw new ArgumentException($"Разрешение для Employee {permission.EmployeeId} и Resource {permission.ResourceId} уже существует");
        
        permission.GrantedDate = DateTime.UtcNow;
        
        _context.AccessPermissions.Add(permission);
        await _context.SaveChangesAsync();
        return permission;
    }

    public async Task UpdateAsync(AccessPermission permission)
    {
        var existingPermission = await GetByIdAsync(permission.Id);
        if (existingPermission != null)
        {
            if (existingPermission.EmployeeId != permission.EmployeeId || existingPermission.ResourceId != permission.ResourceId)
            {
                var duplicateExists = await ExistsForEmployeeAndResourceAsync(
                    permission.EmployeeId, permission.ResourceId);
            
                if (duplicateExists)
                    throw new ArgumentException($"Разрешение для Employee {permission.EmployeeId} и Resource {permission.ResourceId} уже существует");
                
                var employeeExists = await _context.Employees.AnyAsync(e => e.Id == permission.EmployeeId);
                var resourceExists = await _context.Resources.AnyAsync(r => r.Id == permission.ResourceId);
            
                if (!employeeExists || !resourceExists)
                    throw new ArgumentException("Employee или Resource не существует");
            }
            
            existingPermission.EmployeeId = permission.EmployeeId;
            existingPermission.ResourceId = permission.ResourceId;
            existingPermission.AccessLevel = permission.AccessLevel;
            existingPermission.ExpiryDate = permission.ExpiryDate;
            existingPermission.IsActive = permission.IsActive;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var permission = await GetByIdAsync(id);
        if (permission != null)
        {
            _context.AccessPermissions.Remove(permission);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.AccessPermissions.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> ExistsForEmployeeAndResourceAsync(int employeeId, int resourceId)
    {
        return await _context.AccessPermissions.AnyAsync(ap => ap.EmployeeId == employeeId && ap.ResourceId == resourceId);
    }

    public async Task<IEnumerable<AccessPermission>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.AccessPermissions.Where(ap => ap.EmployeeId == employeeId).ToListAsync();
    }

    public async Task<IEnumerable<AccessPermission>> GetByResourceIdAsync(int resourceId)
    {
        return await _context.AccessPermissions.Where(ap => ap.ResourceId == resourceId).ToListAsync();
    }
}