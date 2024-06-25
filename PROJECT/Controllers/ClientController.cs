using Microsoft.AspNetCore.Mvc;
using PROJECT.DTOs;
using PROJECT.Services.Interfaces;

namespace PROJECT.Controllers;

[ApiController]
[Route("api/client/[controller]")]
public class ClientController: ControllerBase
{
    private readonly IClientService _repo;
    public ClientController(IClientService clientService)
    {
        _repo = clientService;
    }

    // 1. add client,
    [HttpPost("add-corporate")]
    public async Task<IActionResult> AddCorporateClient(CorporateClientPostDTO client)
    {
        await _repo.AddCorporateClient(client);
        return Created();
    }
    [HttpPost("add-individual")]
    public async Task<IActionResult> AddIndividualClient(IndividualClientPostDTO client)
    {
        await _repo.AddIndividualClient(client);
        return Created();
    }

    // 2. delete client,
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> RemoveClient(int id)
    {
        try {
            var result = await _repo.RemoveClient(id);
        } catch (Exception e){
            return BadRequest(e.Message);
        }
        return Ok("client deleted");
    }

    // 3. update data about client
    [HttpPut("update-corporate/{id}")]
    public async Task<IActionResult> UpdateClient(int id, CorporateClientUpdateDTO client)
    {
        await _repo.UpdateClient(id, client);
        return Ok("Client updated");
    }
    
    [HttpPut("Update-individual/{id}")]
    public async Task<IActionResult> UpdateClient(int id, IndividualClientUpdateDTO client)
    {
        await _repo.UpdateClient(id, client);
        return Ok("Client updated");
    }

}