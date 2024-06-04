namespace Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class PrescriptionMedicament
{
    public int PrescriptionId { get; set; }
    public int MedicamentId { get; set; }
    int Dose { get; set; }
    [Required]
    [MaxLength(100)]
    string Details { get; set; }
    [ForeignKey(nameof(MedicamentId))]
    public Medicament Medicament { get; set; } = null!;
    [ForeignKey(nameof(PrescriptionId))]
    public Prescription Prescription { get; set; } = null!;
}