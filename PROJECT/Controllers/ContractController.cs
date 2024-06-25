using Microsoft.AspNetCore.Mvc;
using PROJECT.DTOs;
using PROJECT.Services.Interfaces;

namespace PROJECT.Controllers;

[ApiController]
[Route("api/contract/[controller]")]
public class ContractController: ControllerBase
{
    private readonly IContractService _repo;
    public ContractController(IContractService contractService)
    {
        _repo = contractService;
    }

    // 4. create contract
    [HttpPost("create")]
    public async Task<IActionResult> CreateContract(ContractDTO contract)
    {
        await _repo.CreateContract(contract);
        return Ok();
    }

    // 5. issue payment for the contract
    [HttpPost("add-payment/{contractId}")]
    public async Task<IActionResult> IssuePayment(int contractId, PaymentDTO payment)
    {
        try{
            await _repo.IssuePayment(contractId, payment);
        } catch (Exception e){
            return BadRequest(e.Message);
        }
        return Ok();
    }
}