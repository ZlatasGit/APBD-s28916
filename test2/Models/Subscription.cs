using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Subscription")]
public class Subscription
{
    [Key]
    public int IdSubscription { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, 6, ErrorMessage = "Renewal period must be either 1, 3, or 6.")]
    public int RenewalPeriod { get; set; }

    [Required]
    public DateOnly EndTime { get; set; }

    [Required]
    public int Price { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public ICollection<Sale> Sales { get; set; }
}