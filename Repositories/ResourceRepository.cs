using AccessControlAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessControlAPI.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _context;

    public ResourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await _context.Resources.ToListAsync();
    }

    public async Task<Resource?> GetByIdAsync(int id)
    {
        return await _context.Resources.FindAsync(id);
    }

    public async Task<Resource> AddAsync(Resource resource)
    {
        resource.CreatedDate = DateTime.UtcNow;
    
        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();
        return resource;
    }

    public async Task UpdateAsync(Resource resource)
    {
        var existingResource = await GetByIdAsync(resource.Id);
        if (existingResource != null)
        {
            existingResource.Name = resource.Name;
            existingResource.Description = resource.Description;
            existingResource.Type = resource.Type;
            existingResource.IpAddress = resource.IpAddress;
            existingResource.IsActive = resource.IsActive;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var resource = await GetByIdAsync(id);
        if (resource != null)
        {
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Resources.AnyAsync(e => e.Id == id);
    }
}