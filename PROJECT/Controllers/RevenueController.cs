namespace PROJECT.Controllers;
using Microsoft.AspNetCore.Mvc;
using PROJECT.Services.Interfaces;

[ApiController]
[Route("api/revenue/[controller]")]
public class RevenueController: ControllerBase
{
    private readonly IRevenueService _repo;
    public RevenueController(IRevenueService revenueService)
    {
        _repo = revenueService;
    }

    // 7. Calculate current revenue
    [HttpGet("current")]
    public async Task<IActionResult> GetRevenue(int? softwareId, string? currency)
    {
        var result = "";
        try
        {
            result = await _repo.GetRevenue(softwareId, currency);
        } catch (Exception e){
            return BadRequest(e.Message);
        }
        return Ok(result);
    }

    // 8. Calculate predicted revenue(For the entire company. For a specific product.For a specific currency)
    [HttpGet("predicted")]
    public async Task<IActionResult> GetPredictedRevenue(int? softwareId, string? currency)
    {
        var result = "";
        try
        {
            result = await _repo.GetPredictedRevenue(softwareId, currency);
        } catch (Exception e){
            return BadRequest(e.Message);
        }
        return Ok(result);
    }
}