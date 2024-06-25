namespace Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class ClientCategory
{
    [Key]
    public int IdClientCategory { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Required]
    public string DiscountPercent { get; set; }

    public ICollection<Client> Clients { get; set; }
}
