using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT.Models;

[Table("Clients")]
public abstract class AbstractClient(string address, string email, string phone)
{
    [Key]
    public int IdClient { get; set; }
    [Required]
    public string Address { get; set; } = address;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = email;
    [Required]
    [Phone(ErrorMessage = "Phone number must be in format +48123456789")]
    public string Phone { get; set; } = phone;
    public ICollection<Contract> Contracts { get; set; } = [];
}