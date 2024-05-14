using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Controllers;
[Route("api/")]
[ApiController]
public class DoctorController : ControllerBase
{
    
    private IDoctorRepository _doctorRepository;

    public DoctorController(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    // an endpoint that will return data about a particular doctor and a list of prescriptions.
    // Endpoint should take doctor's id as parameter. Prescription data should be returned in descending
    // order by the Date column. Remember to return the appropriate error codes.
    [HttpGet("doctors/{id}")]
    public async Task<IActionResult> GetDoctorsPrescriptions(int id)
    {
        if (!await _doctorRepository.FindDoctor(id))
        {
            return NotFound($"Doctor with id {id} does not exist.");
        } else{
            return Ok(await _doctorRepository.GetDoctorsPrescriptions(id));
        }
    }

    // an endpoint that will allow you to delete data about a given doctor. In a situation when a
    // given doctor has issued some prescriptions, then we also want to delete them (together with data
    // from the association table). Remember that the endpoint should use the transaction to maintain
    // database consistency
    [HttpDelete("doctors/{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        if (!await _doctorRepository.FindDoctor(id))
        {
            return NotFound($"Doctor with id {id} does not exist.");
        } else{
            await _doctorRepository.DeleteDoctor(id);
            return Ok($"Doctor with id {id} has been deleted.");
        }
    }
}