using lab8.Models;

namespace lab8.DTOs;
public class TripDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryTrips> CountryTrips { get; set; }
    public List<ClientDTO> Clients { get; set; }
}