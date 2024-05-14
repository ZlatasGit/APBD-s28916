
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Models;
public class DoctorRepository : IDoctorRepository
{
    private readonly IConfiguration _configuration;
    public DoctorRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /*
    public async Task AddDoctor(Doctor doctor)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = "insert into Doctor(IdDoctor, FirstName, LastName, Email) values(@IdDoctor, @FirstName, @LastName, @Email)";
        command.Parameters.AddWithValue("@IdDoctor", doctor.IdDoctor);
        command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
        command.Parameters.AddWithValue("@LastName", doctor.LastName);
        command.Parameters.AddWithValue("@Email", doctor.Email);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteDoctor(int id)
    {
        var connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();
        var command = new SqlCommand();

        DbTransaction transaction = await connection.BeginTransactionAsync();
        command.Transaction = (SqlTransaction)transaction;

        try
        {
            command.Connection = connection;
            command.CommandText = "delete from Doctor where IdDoctor = @IdDoctor";
            command.Parameters.AddWithValue("@IdDoctor", id);

            await command.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }*/
    public async Task<DoctorDTO> GetDoctorsPrescriptions(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT d.IdDoctor, d.FirstName, d.LastName, d.Email, p.IdPrescription, p.Date FROM Doctor d INNER JOIN Prescription p ON d.IdDoctor = p.IdDoctor WHERE d.IdDoctor = @Id ORDER BY p.Date DESC";
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        var doctor = new DoctorDTO()
        {
            IdDoctor = reader.GetInt32(reader.GetOrdinal("IdDoctor")),
            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
            LastName = reader.GetString(reader.GetOrdinal("LastName")),
            Email = reader.GetString(reader.GetOrdinal("Email"))
        };
        var prescriptions = new List<int>();
        while (await reader.ReadAsync())
        {
            var prescriptionId = reader.GetInt32(reader.GetOrdinal("IdPrescription"));
            // var prescription = new Prescription()
            // {
            //     IdPrescription = reader.GetInt32(reader.GetOrdinal("IdPrescription")),
            //     Date = reader.GetDateTime(reader.GetOrdinal("Date"))
            // };
            prescriptions.Add(prescriptionId);
        }
        doctor.Prescriptions = prescriptions;
        return doctor;
    }

    public async Task<bool> FindDoctor(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Doctor WHERE IdDoctor = @Id";
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();
        return result is not null;
    }

    public async Task<DoctorDTO> GetDoctor(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "select IdDoctor, FirstName, LastName, Email from Doctor where IdDoctor = @Id";
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        var doctor = new DoctorDTO()
        {
            IdDoctor = reader.GetInt32(reader.GetOrdinal("IdDoctor")),
            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
            LastName = reader.GetString(reader.GetOrdinal("LastName")),
            Email = reader.GetString(reader.GetOrdinal("Email"))
        };
        return doctor;
    }

    public async Task DeleteDoctor(int id)
    {
        var delete_medicament = "DELETE FROM Prescription_Medicament WHERE IdPrescription IN (SELECT IdPrescription FROM Prescription WHERE IdDoctor = @IdDoctor)";
        var delete_prescription = "DELETE FROM Prescription WHERE IdDoctor = @IdDoctor";
        var delete_doctor = "DELETE FROM Doctor WHERE IdDoctor = @IdDoctor";
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        
        await connection.OpenAsync();
        
        DbTransaction transaction = await connection.BeginTransactionAsync();
        command.Transaction = (SqlTransaction)transaction;
        try
        {
            command.Parameters.Clear();
            command.CommandText = delete_medicament;
            command.Parameters.AddWithValue("@IdDoctor", id);
            await command.ExecuteNonQueryAsync();
            
            command.Parameters.Clear();
            command.CommandText = delete_prescription;
            command.Parameters.AddWithValue("@IdDoctor", id);
            await command.ExecuteNonQueryAsync();

            command.Parameters.Clear();
            command.CommandText = delete_doctor;
            command.Parameters.AddWithValue("@IdDoctor", id);
            await command.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    /*

    public async Task<IEnumerable<Doctor>> GetDoctors()
    {
        var connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();

        var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "select * from Doctor";

        var reader = await command.ExecuteReaderAsync();
        var doctors = new List<Doctor>();
        while (await reader.ReadAsync())
        {
            var doctor = new Doctor
            {
                IdDoctor = reader.GetInt32(reader.GetOrdinal("IdDoctor")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Email = reader.GetString(reader.GetOrdinal("Email"))
            };
            doctors.Add(doctor);
        }
        return doctors;
    }

    public async Task UpdateDoctor(Doctor doctor)
    {
        var connection = new SqlConnection(_configuration.GetConnectionString("Docker"));
        await connection.OpenAsync();

        var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "update Doctor set FirstName = @FirstName, LastName = @LastName, Email = @Email where IdDoctor=@IdDoctor";

        DbTransaction transaction = await connection.BeginTransactionAsync();
        command.Transaction = (SqlTransaction)transaction;

        try{
            command.Parameters.AddWithValue("@IdDoctor", doctor.IdDoctor);
            command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
            command.Parameters.AddWithValue("@LastName", doctor.LastName);
            command.Parameters.AddWithValue("@Email", doctor.Email);

            await command.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }*/
}