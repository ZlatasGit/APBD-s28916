namespace Models;

public class Prescription
{
    int IdPrescription { get; set; }
    DateTime Date { get; set; }
    DateTime DueDate { get; set; }
    int IdPatient { get; set; }
    int IdDoctor { get; set; }
}