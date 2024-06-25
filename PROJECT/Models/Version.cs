namespace PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Version
{
    [Key]
    public int IdVersion { get; set; }
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
    [Required]
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))]
    public SoftwareSystem Software { get; set; }
    
}