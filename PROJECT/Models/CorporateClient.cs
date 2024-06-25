namespace PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CorporateClient: AbstractClient
{
    public CorporateClient(string address, string email, string phone) : base(address, email, phone)
    {
    }

    public CorporateClient(string address, string email, string phone, string companyName, string krs) : base(address, email, phone)
    {
        CompanyName = companyName;
        KRS = krs;
    }

    [Required]
    public string CompanyName { get; set; }
    [Required]
    [Editable(false)]
    public string KRS { get;}

}