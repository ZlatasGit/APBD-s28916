namespace PROJECT.DTOs;

public class PaymentDTO
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int ContractId { get; set; }
    public bool IsReturned { get; set; }
}