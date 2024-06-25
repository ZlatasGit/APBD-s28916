namespace DTOs;

public class DiscountDTO
{
    public int IdDiscount { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
    public DateOnly EndDate { get; set; }
}