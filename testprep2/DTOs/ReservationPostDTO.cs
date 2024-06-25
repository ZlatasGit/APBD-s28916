namespace DTOs;

public class ReservationPostDTO
{
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public int IdBoatStandard { get; set; }
    public int IdClient { get; set; }
    public int NumOfBoats { get; set; }
}