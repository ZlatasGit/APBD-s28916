namespace Models;
using System.ComponentModel.DataAnnotations;
public class Doctor
{
    [Key]
    int IdDoctor { get; set; }
    [Required]
    [MaxLength(100)]
    string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    string LastName { get; set; }
    [Required]
    [MaxLength(100)]
    string Email { get; set; }
    public virtual ICollection<Prescription> Prescriptions { get; set; } = [];
}