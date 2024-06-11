namespace lab10.DTOs;
{
    //dtos for lab 10

    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginDTO
    {
        public string Userame { get; set; }
        public string Password { get; set; }
    }
    
    public class MedicamentDTO
    {
        public int IdMedicament { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }
    }
    
    public class PrescriptionDTO
    {
        public int IdPatient { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public List<MedicamentDTO> Medicaments { get; set; }
    }
    
    public class RefreshTokenRequestDTO
    {
        public string RefreshToken { get; set; }
    }
    

    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}