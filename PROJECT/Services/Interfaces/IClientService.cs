namespace PROJECT.Services.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;

public interface IClientService
{
    // 1. add client
    Task<IActionResult> AddCorporateClient(CorporateClientPostDTO client);
    Task<IActionResult> AddIndividualClient(IndividualClientPostDTO client);
    
    // 2. remove client
    Task<IActionResult> RemoveClient(int id);

    // 3. update data about client
    Task<IActionResult> UpdateClient(int id, IndividualClientUpdateDTO client);
    Task<IActionResult> UpdateClient(int id, CorporateClientUpdateDTO client);
}
