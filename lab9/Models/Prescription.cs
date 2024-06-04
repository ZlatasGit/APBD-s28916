namespace Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    [ForeignKey(nameof(IdDoctor))]
    public virtual Doctor Doctor { get; set; } = null!;
    [ForeignKey(nameof(IdPatient))]
    public virtual Patient Patient { get; set; } = null!;
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = [];

}