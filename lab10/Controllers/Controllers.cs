using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace lab10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MyContext _context;

        public AuthController(IConfiguration configuration, MyContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(RegisterUserDTO model)
        {
            if (_context.Users.Any(u => u.UserName == model.UserName))
                return BadRequest("User already exists");

            AppUser user = new AppUser()
            {
                Username = model.Username,
                Password = model.Password,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpirationDate = DateTime.Today.AddDays(1)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginRequest)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == loginRequest.Username
                                                           && u.Password == loginRequest.Password);
            if (user == null)
                return Unauthorized();

            var token = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = DateTime.Now.AddDays(7);

            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }

        private string GenerateAccessToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshToken == refreshTokenRequestDto.RefreshToken);
            if (user == null || user.RefreshTokenExpirationDate <= DateTime.Now)
                return Unauthorized();

            var newJwtToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpirationDate = DateTime.Now.AddDays(7);

            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            });
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly MyContext _context;

        public PatientController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientDetails(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.PatientPrescriptions)
                .ThenInclude(pr => pr.IdDoctorPrescription)
                .Include(p => p.PatientPrescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
                .FirstOrDefaultAsync(p => p.IdPatient == patientId);

            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            var response = new
            {
                PatientId = patient.IdPatient,
                Firsname = patient.Firstname,
                Lastname = patient.Lastname,
                Birthdate = patient.Birthdate,
                Prescriptions = patient.PatientPrescriptions
                    .OrderBy(p => p.DueDate)
                    .Select(p => new
                    {
                        PrescriptionId = p.IdPrescription,
                        Doctor = new
                        {
                            DoctorId = p.IdDoctorPrescription.IdDoctor,
                            Firstname = p.IdDoctorPrescription.FirstName,
                            Lastname = p.IdDoctorPrescription.LastName,
                            Email = p.IdDoctorPrescription.Email
                        },
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Medications = p.PrescriptionMedicaments
                            .Select(pm => new
                            {
                                MedicamentId = pm.Medicament.IdMedicament,
                                Name = pm.Medicament.Name,
                                Description = pm.Medicament.Description,
                                Type = pm.Medicament.Type,
                                Dose = pm.Dose,
                                Details = pm.Details
                            }).ToList()
                    }).ToList()
            };

            return Ok(response);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly MyContext _context;

        public PrescriptionController(MyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> IssuePrescription(PrescriptionDTO request)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == request.PatientId);
            if (patient == null)
            {
                patient = new Patient
                {
                    IdPatient = request.PatientId,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Birthdate = request.Birthdate
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            foreach (var medication in request.Medicaments)
            {
                var medicament = await _context.Medicaments.FindAsync(medication.MedicamentId);
                if (medicament == null)
                {
                    return BadRequest($"Medicament with ID {medication.MedicamentId} does not exist.");
                }
            }

            if (request.Medicaments.Count > 10)
            {
                return BadRequest("A prescription can include a maximum of 10 medications.");
            }

            if (request.DueDate < request.Date)
            {
                return BadRequest("DueDate must be greater than or equal to Date.");
            }

            var prescription = new Prescription
            {
                IdDoctor = request.DoctorId,
                IdPatient = request.PatientId,
                Date = request.Date,
                DueDate = request.DueDate
            };
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            foreach (var medication in request.Medicaments)
            {
                var prescriptionMedicament = new PrescriptionMedicament
                {
                    IdPrescription = prescription.IdPrescription,
                    IdMedicament = medication.MedicamentId,
                    Dose = medication.Dose,
                    Details = medication.Details
                };
                _context.PrescriptionMedicaments.Add(prescriptionMedicament);
            }

            await _context.SaveChangesAsync();

            return Ok("Prescription issued successfully.");
        }
    }
}
