
namespace lab8.DTOs;
public class ClientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
    public List<TripDTO> Trips { get; set; }
    public DateTime? PaymentDate { get; internal set; }
}