using api.Dtos;
using api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : Controller
{
    private readonly IClientService _clientService;

    public ClientsController(IServiceManager serviceManager)
    {
        _clientService = serviceManager.ClientService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var services = await _clientService.FindClientsAsync();
        return Ok(services);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var service = await _clientService.FindClientAsync(id);
            return Ok(service);
        }
        catch (NullReferenceException exception)
        {
            return NotFound(new { message = $"{id} was not found" });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClientWriteDto serviceWriteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var id = await _clientService.CreateClientAsync(serviceWriteDto);
        return Created($"/services/{id}", null);
    }
    
    [HttpPut("{id}")] 
    public async Task<IActionResult> Put(Guid id, [FromBody] ClientWriteDto serviceWriteDto)
    {
        try
        {
            await _clientService.UpdateClientAsync(id, serviceWriteDto);
            return NoContent();
        }
        catch (NullReferenceException exception)
        {
            return NotFound(new { message = $"id {id} was not found" });
        }
    }
}