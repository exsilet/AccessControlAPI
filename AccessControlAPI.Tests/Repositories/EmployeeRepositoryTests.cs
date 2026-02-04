using AccessControlAPI.Models;
using AccessControlAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AccessControlAPI.Tests;

public class EmployeeRepositoryTests
{
    private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new ApplicationDbContext(options);
    }
    
    [Fact]
    public async Task AddAsync_ShouldAddEmployeeToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        var repository = new EmployeeRepository(context);
        
        var employee = new Employee 
        { 
            FirstName = "Иван",
            LastName = "Иванов",
            Email = "ivan@test.com",
            Position = "Разработчик",
            Department = "IT",
            HireDate = DateTime.UtcNow,
            IsActive = true
        };
        
        // Act
        var result = await repository.AddAsync(employee);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Иван", result.FirstName);
        Assert.True(result.Id > 0);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnEmployee_WhenExists()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        var repository = new EmployeeRepository(context);
        
        var employee = new Employee 
        { 
            FirstName = "Тест",
            LastName = "Пользователь",
            Email = "test@test.com",
            Position = "Тестировщик",
            Department = "QA",
            HireDate = DateTime.UtcNow,
            IsActive = true
        };
        
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
        
        // Act
        var result = await repository.GetByIdAsync(employee.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(employee.Id, result.Id);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenEmployeeNotExists()
    {
        // Arrange
        using var context = CreateInMemoryDbContext();
        var repository = new EmployeeRepository(context);
        
        // Act
        var result = await repository.GetByIdAsync(999);
        
        // Assert
        Assert.Null(result);
    }
}