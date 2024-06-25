using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Payment")]
public class Payment
{
    [Key]
    public int IdPayment { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    public int Value { get; set; }
    public int SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public Subscription Subscription { get; set; }

    public int ClientId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
}