namespace Services.Interfaces;

using System.Collections.Generic;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

public interface IClientService
{
    Task<IEnumerable<ClientGetDTO>> GetAllClients();
    Task<ClientGetDTO?> GetClientById(int id);
    Task<ClientGetDTO> AddClient(ClientPostDTO clientPostDTO);
    Task<ClientGetDTO?> UpdateClient(int id, ClientPostDTO clientPutDTO);
    Task<bool> DeleteClient(int id);
}