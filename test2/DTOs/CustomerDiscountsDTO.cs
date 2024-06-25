namespace DTOs;

public class CustomerDiscountsDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public IEnumerable<DiscountDTO> Discounts { get; set; }
}