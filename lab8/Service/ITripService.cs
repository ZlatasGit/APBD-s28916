using lab8.DTOs;

public interface ITripService
{
    Task<(IEnumerable<TripDTO> trips, int totalPages)> GetTripsAsync(int page, int pageSize);
    Task<bool> DeleteClientAsync(int clientId);
    Task<bool> AssignClientToTripAsync(int tripId, ClientDTO clientDto);
}
