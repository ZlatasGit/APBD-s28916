namespace DTOs;

public class ClientGetDTO
{
    public int IdClient { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public string Pesel { get; set; }
    public string Email { get; set; }
    public string ClientCategoryName { get; set; }
    public IEnumerable<ReservationGetDTO> Reservations { get; set; }
}