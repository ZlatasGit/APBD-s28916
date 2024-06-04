namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using Models;
using Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly PrescriptionDbContext _context;

    public PatientController(PrescriptionDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
            .Where(p => p.IdPatient == id)
            .FirstOrDefaultAsync();

        if (patient == null)
        {
            return NotFound();
        }

        var result = new
        {
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(p => new
            {
                p.IdPrescription,
                p.Date,
                p.DueDate,
                Medicaments = p.PrescriptionMedicaments.Select(pm => new
                {
                    pm.IdMedicament,
                    pm.Medicament.Name,
                    pm.Medicament.Description,
                    pm.Medicament.Type
                }),
                Doctor = new
                {
                    p.Doctor.IdDoctor,
                    p.Doctor.FirstName,
                    p.Doctor.LastName,
                    p.Doctor.Email
                }
            }).OrderBy(p => p.DueDate)
        };

        return Ok(result);
    }
}