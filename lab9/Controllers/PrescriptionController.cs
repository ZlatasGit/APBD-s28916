namespace Controllers;

using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly PrescriptionService _service;

    public PrescriptionsController(PrescriptionService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrescriptionDto>> GetPatientData(int id)
    {
        return await _service.GetPatientData(id);
    }

    [HttpPost]
    public async Task<ActionResult> AddPrescription(PrescriptionDto request)
    {
        await _service.AddPrescription(request);
        return Ok();
    }
}