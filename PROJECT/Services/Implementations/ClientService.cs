using Microsoft.AspNetCore.Mvc;
using PROJECT.Context;
using PROJECT.DTOs;
using PROJECT.Services.Interfaces;
using PROJECT.Models;

namespace PROJECT.Services.Implementations;

public class ClientService : IClientService
{
    private readonly SystemDbContext _repository;

    public ClientService(SystemDbContext clientRepository)
    {
        _repository = clientRepository;
    }
    
    public async Task<IActionResult> AddCorporateClient(CorporateClientPostDTO client)
    {
        var corporateClient = new CorporateClient(
            address: client.Address,
            email: client.Email,
            phone: client.Phone,
            companyName: client.CorporateName,
            krs: client.KRS
        );

        await _repository.CorporateClients.AddAsync(corporateClient);
        await _repository.SaveChangesAsync();
        return new CreatedResult();
    }
    public async Task<IActionResult> AddIndividualClient(IndividualClientPostDTO client)
    {
        var individualClient = new IndividualClient(
            address: client.Address,
            email: client.Email,
            phone: client.Phone,
            firstName: client.FirstName,
            lastName: client.LastName,
            pesel: client.PESEL
        );

        await _repository.IndividualClients.AddAsync(individualClient);
        await _repository.SaveChangesAsync();
        return new CreatedResult();
    }

    public async Task<IActionResult> RemoveClient(int id)
    {
        var corporateClient = await _repository.CorporateClients.FindAsync(id);
        if (corporateClient != null)
        {
            throw new Exception("Cannot delete corporate clients.");
        }

        var individualClient = await _repository.IndividualClients.FindAsync(id);
        if (individualClient != null)
        {
            individualClient.IsDeleted = true;
            await _repository.SaveChangesAsync();
            return new OkResult();
        }

        return new BadRequestResult();
    }

    public async Task<IActionResult> UpdateClient(int id, IndividualClientUpdateDTO client)
    {
        var individualClient = await _repository.IndividualClients.FindAsync(id);
        if (individualClient != null)
        {
            individualClient.Address = client.Address;
            individualClient.Email = client.Email;
            individualClient.Phone = client.Phone;
            individualClient.FirstName = client.FirstName;
            individualClient.LastName = client.LastName;

            await _repository.SaveChangesAsync();
            return new OkResult();
        }
        return new BadRequestResult();
    }
    public async Task<IActionResult> UpdateClient(int id, CorporateClientUpdateDTO client)
    {
        var corporateClient = await _repository.CorporateClients.FindAsync(id);
        if (corporateClient != null)
        {
            corporateClient.Address = client.Address;
            corporateClient.Email = client.Email;
            corporateClient.Phone = client.Phone;
            corporateClient.CompanyName = client.CorporateName;

            await _repository.SaveChangesAsync();
            return new OkResult();
        }
        return new BadRequestResult();
    }
}
