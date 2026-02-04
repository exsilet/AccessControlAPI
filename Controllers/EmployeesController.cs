using AccessControlAPI.Models;
using AccessControlAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeesController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    // GET: api/employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employees = await _repository.GetAllAsync();
        return Ok(employees);
    }

    // GET: api/employees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        
        if (employee == null)
            return NotFound();
        
        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var createdEmployee = await _repository.AddAsync(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
            return BadRequest("ID в пути не совпадает с ID в теле запроса");
    
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var exists = await _repository.ExistsAsync(id);
        if (!exists) 
            return NotFound();
    
        await _repository.UpdateAsync(employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var exists = await _repository.ExistsAsync(id);
        if (!exists)
            return NotFound();
        
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}