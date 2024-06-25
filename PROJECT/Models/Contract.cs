namespace PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contract
{

    [Key]
    public int IdContract { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    public double Amount { get; set; }

    [Required(ErrorMessage = "Client is required")]
    public int ClientId { get; set; }

    [Required]
    [ForeignKey(nameof(ClientId))]
    public AbstractClient Client { get; set; }

    [Required]
    public int SoftwareSystemId { get; set; }

    [Required]
    [ForeignKey(nameof(SoftwareSystemId))]
    public SoftwareSystem SoftwareSystem { get; set; }

    [Required]
    public string SoftwareVersion { get; set; } 

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    [Range(3, 30, ErrorMessage = "Time range must be between 3 and 30 days")]
    public int Duration { get; set; } 

    [Required]
    public DateOnly EndDate { get; set; }

    [Range(1,3, ErrorMessage = "Number of extension years must be between 1 and 3")]
    public int? ExtendUpdatesBy { get; set;} 

    public ICollection<Payment> Payments { get; set; } = [];
}