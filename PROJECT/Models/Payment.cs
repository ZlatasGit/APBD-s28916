namespace PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    public int IdPayment { get; set; }
    [Required]
    public double Amount { get; set; }
    [Required]
    public int ContractId { get; set; }
    [Required]
    [ForeignKey(nameof(ContractId))]
    public Contract Contract { get; set; }
    [Required]
    public DateOnly PaymentDate { get; set; }
    [Required]
    [Editable(true)]
    public bool IsReturned { get; set; } = false;
}