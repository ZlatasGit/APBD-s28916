namespace Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Client
{
    [Key]
    public int IdClient { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateOnly Birthday { get; set; }

    [MaxLength(100)]
    [Required]
    public string Pesel { get; set; }

    [MaxLength(100)]
    [Required]
    public string Email { get; set; }

    public int ClientCategoryId { get; set; }

    [ForeignKey(nameof(ClientCategoryId))]
    public ClientCategory ClientCategory { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}
