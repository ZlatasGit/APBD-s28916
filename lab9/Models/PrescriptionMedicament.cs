namespace Models;
public class PrescriptionMedicament
{
    int PrescriptionId { get; set; }
    int MedicamentId { get; set; }
    int Dose { get; set; }
    string Details { get; set; }
}