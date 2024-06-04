using DTOs;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Services;

    public class PrescriptionService
    {
        private readonly PrescriptionDbContext _context;

        public PrescriptionService(PrescriptionDbContext context)
        {
            _context = context;
        }
                
        public async Task AddPrescription(PrescriptionDto request)
        {
            // Validate the request data
            if (request.Medicaments.Count > 10)
            {
                throw new ArgumentException("A prescription can include a maximum of 10 medications.");
            }

            if (request.DueDate < request.Date)
            {
                throw new ArgumentException("DueDate must be greater than or equal to Date.");
            }

            // Check if the patient exists in the database
            var patient = await _context.Patients.FindAsync(request.IdPatient);
            if (patient == null)
            {
                // If not, create a new patient
                patient = new Patient
                {
                    FirstName = request.PatientFirstName,
                    // Set other properties...
                };
                _context.Patients.Add(patient);
            }

            // Check if the medicaments exist in the database
            foreach (var medicamentDto in request.Medicaments)
            {
                var medicament = await _context.Medicaments.FindAsync(medicamentDto.IdMedicament);
                if (medicament == null)
                {
                    throw new ArgumentException($"Medicament with Id {medicamentDto.IdMedicament} does not exist.");
                }
            }

            // Create a new prescription
            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                // Set other properties...
            };
            _context.Prescriptions.Add(prescription);

            // Save changes in the database
            await _context.SaveChangesAsync();
        }

        public async Task<PrescriptionDto> GetPatientData(int id)
        {
            // Retrieve the patient data from the database
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
                throw new ArgumentException($"Patient with Id {id} does not exist.");
            }

            // Map the patient data to the response DTO
            var response = new PrescriptionDto
            {
                IdPatient = patient.IdPatient,
                PatientFirstName = patient.FirstName,
                // Set other properties...
                Prescriptions = patient.Prescriptions.Select(p => new PrescriptionDto
                {
                    Date = p.Date,
                    DueDate = p.DueDate,
                    // Set other properties...
                }).OrderBy(p => p.DueDate).ToList()
            };

            return response;
        }
    }