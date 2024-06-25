namespace Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Sailboat
{
    [Key]
    public int IdSailboat { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Required]
    public int Capacity { get; set; }

    [MaxLength(100)]
    [Required]
    public string Description { get; set; }

    public int? BoatStandardId { get; set; }

    [Required]
    public int Price { get; set; }

    public ICollection<Reservation> Reservations { get; set; }

    [ForeignKey(nameof(BoatStandardId))]
    public BoatStandard? BoatStandard { get; set; }
}
