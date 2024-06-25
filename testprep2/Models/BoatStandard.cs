namespace Models;
using System.ComponentModel.DataAnnotations;
public class BoatStandard
{
    [Key]
    public int IdBoatStandard { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Required]
    public int Level { get; set; }

    public ICollection<Sailboat> Sailboats { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}

