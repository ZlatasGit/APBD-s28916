namespace PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class IndividualClient : AbstractClient
{
    public IndividualClient(string address, string email, string phone) : base(address, email, phone)
    {
    }

    public IndividualClient(string address, string email, string phone, string firstName, string lastName, string pesel) : base(address, email, phone)
    {
        FirstName = firstName;
        LastName = lastName;
        PESEL = pesel;
    }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [RegularExpression("([0-9]{11})", ErrorMessage = "PESEL must be 11 digits long")]
    [Editable(false)]
    public string PESEL { get; }
    [Required]
    [Editable(true)]
    public bool IsDeleted { get; set; } = false;
}