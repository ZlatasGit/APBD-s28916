namespace Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Reservation
{
    [Key]
    public int IdReservation { get; set; }

    public int ClientId { get; set; }

    public int BoatStandardId { get; set; }

    [Required]
    public DateOnly DateFrom { get; set; }

    [Required]
    public DateOnly DateTo { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public int NumOfBoats { get; set; }

    [Required]
    public bool Fulfilled { get; set; }

    public int? Price { get; set; }

    [MaxLength(100)]
    public string? CancelReason { get; set; }

    public ICollection<Sailboat> Sailboats { get; set; }

    [ForeignKey(nameof(ClientId))]    
    public Client Client { get; set; }

    [ForeignKey(nameof(BoatStandardId))]
    public BoatStandard BoatStandard { get; set; }
}
