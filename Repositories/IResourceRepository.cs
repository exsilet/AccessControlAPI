using AccessControlAPI.Models;

namespace AccessControlAPI.Repositories;

public interface IResourceRepository
{
    Task<IEnumerable<Resource>> GetAllAsync();
    Task<Resource?> GetByIdAsync(int id);
    Task<Resource> AddAsync(Resource resource);
    Task UpdateAsync(Resource resource);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}