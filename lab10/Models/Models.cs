lab10.Models{

    // models for lab 10
    public class AppUser : IdentityUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
    
    public partial class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }
        
        [MaxLength(100)]
        public string Firstname { get; set; } = null!;

        [MaxLength(100)]
        public string Lastname { get; set; } = null!;

        [MaxLength(100)]
        public string Email { get; set; } = null!;
        
        [InverseProperty("IdDoctorPrescription")]
        public virtual ICollection<Prescription> DoctorPrescriptions { get; set; } = new List<Prescription>();
    }
    
    public class IssuePrescriptionRequest
    {
        public int PatientId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<MedicationRequest> Medications { get; set; }
    }
    
    public partial class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Description { get; set; } = null!;

        [MaxLength(100)]
        public string Type { get; set; } = null!;
        
        [InverseProperty("Medicament")]
        public virtual ICollection<PrescriptionMedicament> MedicamentPrescriptions { get; set; } = new List<PrescriptionMedicament>();
    }
    
    public class MedicationRequest
    {
        public int IdMedicament { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }
    }
    
    public partial class Patient
    {
        [Key]
        public int IdPatient { get; set; }
        
        [MaxLength(100)]
        public string Firstname { get; set; } = null!;

        [MaxLength(100)]
        public string Lastname { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime Birthdate { get; set; }
        
        [InverseProperty("IdPatientPrescription")]
        public virtual ICollection<Prescription> PatientPrescriptions { get; set; } = new List<Prescription>();
    }
    
    public partial class Prescription
    {
        [Key]
        public int IdPrescription { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        
        [Column(TypeName = "datetime")]
        public DateTime DueDate { get; set; }
        
        public int IdPatient { get; set; }
        
        public int IdDoctor { get; set; }
        
        [InverseProperty("Prescription")]
        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
        
        [ForeignKey(nameof(IdDoctor)]
        [InverseProperty("DoctorPrescriptions")]
        public virtual Doctor IdDoctorPrescription { get; set; } = null!;
        
        [ForeignKey(nameof(IdPatient)]
        [InverseProperty("PatientPrescriptions")]
        public virtual Patient IdPatientPrescription { get; set; } = null!;
    }
    
    public partial class PrescriptionMedicament
    {
        [Key]
        public int IdPrescription { get; set; }
        
        [Key]
        public int IdMedicament { get; set; }
        
        public int? Dose { get; set; }
        
        [MaxLength(100)]
        public string Details { get; set; } = null!;
        
        [ForeignKey(nameof(IdPrescription))]
        [InverseProperty("PrescriptionMedicaments")]
        public virtual Prescription Prescription { get; set; } = null!;
        
        [ForeignKey(nameof(IdMedicament))]
        [InverseProperty("MedicamentPrescriptions")]
        public virtual Medicament Medicament { get; set; } = null!;
    }
}