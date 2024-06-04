namespace DTOs;
public class PrescriptionDto
{
    public int IdPatient { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public DateOnly PatientBirthdate { get; set; }
    public int DoctorId { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public List<PrescriptionDto> Prescriptions { get; set; }
}