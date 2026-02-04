using AccessControlAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessControlAPI;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<AccessPermission> AccessPermissions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}