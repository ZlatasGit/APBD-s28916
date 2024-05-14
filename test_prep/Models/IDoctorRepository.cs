namespace Models;
public interface IDoctorRepository
{
    Task<DoctorDTO> GetDoctor(int id);
    Task<DoctorDTO> GetDoctorsPrescriptions(int id);
    Task<bool> FindDoctor(int id);
    Task DeleteDoctor(int id);
}