namespace Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class PrescriptionMedicament
{
    public int IdPrescription { get; set; }
    public int IdMedicament { get; set; }
    int Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicament { get; set; } = null!;
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; } = null!;
}