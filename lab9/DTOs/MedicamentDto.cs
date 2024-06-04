namespace DTOs;
using System.ComponentModel.DataAnnotations;
public class MedicamentDto
{
    [Key]
    public int MedicamentId { get; set; }
    public int Dose { get; set; }
}