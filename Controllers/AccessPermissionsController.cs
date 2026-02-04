using AccessControlAPI.Models;
using AccessControlAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessPermissionsController : ControllerBase
{
    private readonly IAccessPermissionRepository _repository;

    public AccessPermissionsController(IAccessPermissionRepository repository)
    {
        _repository = repository;
    }

    // GET: api/accesspermissions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccessPermission>>> GetAccessPermissions()
    {
        var permissions = await _repository.GetAllAsync();
        return Ok(permissions);
    }

    // GET: api/accesspermissions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AccessPermission>> GetAccessPermission(int id)
    {
        var permission = await _repository.GetByIdAsync(id);
        
        if (permission == null)
            return NotFound();
        
        return Ok(permission);
    }

    // POST: api/accesspermissions
    [HttpPost]
    public async Task<ActionResult<AccessPermission>> CreateAccessPermission([FromBody] AccessPermission permission)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var createdPermission = await _repository.AddAsync(permission);
            return CreatedAtAction(nameof(GetAccessPermission), 
                new { id = createdPermission.Id }, 
                createdPermission);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    // PUT: api/accesspermissions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccessPermission(int id, [FromBody] AccessPermission permission)
    {
        if (id != permission.Id)
            return BadRequest("ID в пути не совпадает с ID в теле запроса");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            await _repository.UpdateAsync(permission);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    // DELETE: api/accesspermissions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccessPermission(int id)
    {
        var exists = await _repository.ExistsAsync(id);
        if (!exists)
            return NotFound();
        
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    // GET: api/accesspermissions/employee/5
    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<AccessPermission>>> GetByEmployeeId(int employeeId)
    {
        var permissions = await _repository.GetByEmployeeIdAsync(employeeId);
        return Ok(permissions);
    }

    // GET: api/accesspermissions/resource/5
    [HttpGet("resource/{resourceId}")]
    public async Task<ActionResult<IEnumerable<AccessPermission>>> GetByResourceId(int resourceId)
    {
        var permissions = await _repository.GetByResourceIdAsync(resourceId);
        return Ok(permissions);
    }
}