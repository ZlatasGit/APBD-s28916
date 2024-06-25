using Microsoft.AspNetCore.Mvc;
using PROJECT.Context;
using PROJECT.DTOs;
using PROJECT.Services.Interfaces;
using PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace PROJECT.Services.Implementations;

public class ContractService : IContractService
{
    private readonly SystemDbContext _repository;

    public ContractService(SystemDbContext contractRepository)
    {
        _repository = contractRepository;
    }

    // 4. create contract
    public async Task<IActionResult> CreateContract(ContractDTO contractDto)
    {
        if (contractDto == null)
        {
            return new BadRequestResult();
        }

        //calculating total discount for client
        var clientContracts = await _repository.Contracts.Where(c => c.ClientId == contractDto.ClientId).ToListAsync();

        var totalDiscount = clientContracts.IsNullOrEmpty() ? 0 : 5;

        var clientDiscount = await _repository.Discounts
            .Where(d => d.StartDate < DateOnly.FromDateTime(contractDto.StartDate)
                && d.EndDate > DateOnly.FromDateTime(contractDto.StartDate.AddDays(contractDto.PaymentInterval)))
            .OrderByDescending(d => d.Value).FirstOrDefaultAsync();
        
        totalDiscount += clientDiscount == null ? 0 : (int)clientDiscount.Value;
        totalDiscount = 1 - totalDiscount;
        var contract = new Contract
        {
            ClientId = contractDto.ClientId,
            Amount = totalDiscount == 1 ? contractDto.Amount : contractDto.Amount*(totalDiscount/100),
            SoftwareSystemId = contractDto.SoftwareId,
            SoftwareVersion = contractDto.VersionId.ToString(),
            StartDate = DateOnly.FromDateTime(contractDto.StartDate),
            Duration = contractDto.PaymentInterval,
            EndDate = DateOnly.FromDateTime(contractDto.StartDate.AddDays(contractDto.PaymentInterval)),
            ExtendUpdatesBy = contractDto.UpdatesExtension
        };

        _repository.Contracts.Add(contract);
        await _repository.SaveChangesAsync();

        return new OkResult();
    }

    // 5. issue payment for the contract
    public async Task<IActionResult> IssuePayment(int contractId, PaymentDTO paymentDto)
    {
        if (paymentDto == null)
        {
            return new BadRequestResult();
        }

        var contract = await _repository.Contracts.FindAsync(contractId);
        if (contract == null)
        {
            return new NotFoundResult();
        }
        
        var totalAmount = await _repository.Payments
            .Where(p => p.ContractId == contractId)
            .SumAsync(p => p.Amount);
        if (totalAmount>=contract.Amount)
        {
            throw new Exception("This contract is already paid off");
        }
        if (DateOnly.FromDateTime(paymentDto.PaymentDate) > contract.EndDate)
        {
            var payments = await _repository.Payments.Where(p => p.ContractId == contractId).ToListAsync();
            foreach (var contractPayment in payments)
            {
                contractPayment.IsReturned = true;
            }
            throw new Exception("The deadline for payment has passed, all payments are returned.");
        }

        var payment = new Payment
        {
            ContractId = contractId,
            Amount = (double)paymentDto.Amount,
            PaymentDate = DateOnly.FromDateTime(paymentDto.PaymentDate),
            IsReturned = paymentDto.IsReturned
        };

        _repository.Payments.Add(payment);
        await _repository.SaveChangesAsync();

        return new OkResult();
    }
}