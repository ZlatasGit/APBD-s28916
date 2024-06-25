namespace PROJECT.Services.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;

public interface IContractService
{
    // 4. create contract
    Task<IActionResult> CreateContract(ContractDTO contract);
    
    // 5. issue payment for the contract
    Task<IActionResult> IssuePayment(int contractId, PaymentDTO payment);
}