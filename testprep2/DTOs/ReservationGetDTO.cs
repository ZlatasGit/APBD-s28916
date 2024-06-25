namespace DTOs;

public class ReservationGetDTO
{
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public string BoatStandardName { get; set; }
    public int Capacity { get; set; }
    public int NumOfBoats { get; set; }
    public bool Fulfilled { get; set; }
    public int? Price { get; set; }
    public string? CancelReason { get; set; }
}