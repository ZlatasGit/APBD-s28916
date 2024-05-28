using lab8.DTOs;
using lab8.Models;
using Microsoft.EntityFrameworkCore;

namespace lab8.Services;
public class TripService : ITripService
{
    private readonly TripManagementContext _context;

    public TripService(TripManagementContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<TripDTO> trips, int totalPages)> GetTripsAsync(int page, int pageSize)
    {
        var tripsQuery = _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Select(t => new TripDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                CountryTrips = t.CountryTrips.Select(ct => ct.IdCountry).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDTO
                {
                    FirstName = ct.Client.FirstName,
                    LastName = ct.Client.LastName
                }).ToList()
            });

        var totalTrips = await tripsQuery.CountAsync();
        var totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

        var trips = await tripsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (trips, totalPages);
    }

    public async Task<bool> DeleteClientAsync(int clientId)
    {
        var client = await _context.Clients.FindAsync(clientId);

        if (client == null || await _context.ClientTrips.AnyAsync(ct => ct.IdClient == clientId))
        {
            return false;
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignClientToTripAsync(int tripId, ClientDTO clientDto)
    {
        if (await _context.Clients.AnyAsync(c => c.Pesel == clientDto.Pesel))
        {
            return false; // Client already exists
        }

        var trip = await _context.Trips.FindAsync(tripId);

        if (trip == null || trip.DateFrom <= DateTime.Now)
        {
            return false; // Trip does not exist or has already started
        }

        var client = new Client
        {
            FirstName = clientDto.FirstName,
            LastName = clientDto.LastName,
            Email = clientDto.Email,
            Telephone = clientDto.Telephone,
            Pesel = clientDto.Pesel
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var clientTrip = new ClientTrips
        {
            IdClient = client.IdClient,
            IdTrip = tripId,
            RegisteredAt = DateTime.Now,
            PaymentDate = clientDto.PaymentDate
        };

        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();

        return true;
    }
}