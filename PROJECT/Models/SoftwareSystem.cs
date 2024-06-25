namespace PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Softwares")]
public class SoftwareSystem
{
    [Key]
    public int IdSoftware { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public SoftwareCategory Category { get; set; }

    public ICollection<Contract> Contracts { get; set; } = [];
    public ICollection<Version> Versions { get; set; } = [];
}

public enum SoftwareCategory
{
    Finance,
    Education,
    Entertainment,
    Business
}