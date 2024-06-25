namespace Services.Implementations;

using DTOs;
using Models;
using Context;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class ClientService : IClientService
{
    private readonly BoatDbContext _context;

    public ClientService(BoatDbContext context)
    {
        _context = context;
    }

    private ClientGetDTO MapToDto(Client client)
    {
        return new ClientGetDTO
        {
            IdClient = client.IdClient,
            Name = client.Name,
            LastName = client.LastName,
            Email = client.Email,
            Pesel = client.Pesel,
            Birthday = client.Birthday,
            ClientCategoryName = client.ClientCategory.Name
        };
    }

    public async Task<IEnumerable<ClientGetDTO>> GetAllClients()
    {
        var clients = await _context.Clients.ToListAsync();
        var clientGetDTOs = new List<ClientGetDTO>();

        foreach (var client in clients)
        {
            var clientGetDTO = MapToDto(client);
            clientGetDTOs.Add(clientGetDTO);
        }

        return clientGetDTOs;
    }

    public async Task<ClientGetDTO?> GetClientById(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == id);
        if (client == null)
        {
            return null;
        }
        return MapToDto(client);
    }

    public async Task<ClientGetDTO> AddClient(ClientPostDTO clientPostDTO)
    {
        var customer = new Client
        {
            Name = clientPostDTO.Name,
            LastName = clientPostDTO.LastName,
            Email = clientPostDTO.Email,
            Pesel = clientPostDTO.Pesel,
            Birthday = clientPostDTO.Birthday,
            ClientCategoryId = clientPostDTO.IdClientCategory
        };

        await _context.Clients.AddAsync(customer);
        await _context.SaveChangesAsync();

        return MapToDto(customer);
    }

    public async Task<ClientGetDTO?> UpdateClient(int id, ClientPostDTO customerPutDTO)
    {
        var customer = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == id);

        if (customer == null)
        {
            return null;
        }

        customer.Name = customerPutDTO.Name;
        customer.LastName = customerPutDTO.LastName;
        customer.Email = customerPutDTO.Email;
        customer.Pesel = customerPutDTO.Pesel;

        await _context.SaveChangesAsync();

        return MapToDto(customer);
    }

    public async Task<bool> DeleteClient(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == id);

        if (client == null)
        {
            return false;
        }

        _context.Reservations.RemoveRange(client.Reservations);
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return true;
    }
}