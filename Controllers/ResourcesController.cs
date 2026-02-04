using AccessControlAPI.Models;
using AccessControlAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccessControlAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourcesController : ControllerBase
{
    private readonly IResourceRepository _repository;

    public ResourcesController(IResourceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
    {
        var resources = await _repository.GetAllAsync();
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Resource>> GetResource(int id)
    {
        var resource = await _repository.GetByIdAsync(id);

        if (resource == null)
            return NotFound();

        return Ok(resource);
    }

    [HttpPost]
    public async Task<ActionResult<Resource>> CreateResource([FromBody] Resource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdResource = await _repository.AddAsync(resource);
        return CreatedAtAction(nameof(GetResource), new { id = createdResource.Id }, createdResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResource(int id, [FromBody] Resource resource)
    {
        if (id != resource.Id)
            return BadRequest("ID в пути не совпадает с ID в теле запроса");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exists = await _repository.ExistsAsync(id);
        if (!exists)
            return NotFound();

        await _repository.UpdateAsync(resource);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResource(int id)
    {
        var exists = await _repository.ExistsAsync(id);
        if (!exists)
            return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}