using PROJECT.Context;
using PROJECT.Services.Interfaces;

namespace PROJECT.Services.Implementations;

public class RevenueService : IRevenueService
{
    private readonly SystemDbContext _repository;
    private readonly HttpClient _httpClient;
    private readonly string _baseCurrency = "PLN";

    public RevenueService(SystemDbContext revenueRepository, HttpClient httpClient)
    {
        _repository = revenueRepository;
        _httpClient = httpClient;
    }

    private async Task<string> ConvertCurrency(double amount, string targetCurrency)
    {
        if (string.IsNullOrEmpty(targetCurrency) || targetCurrency.Length!=3 || targetCurrency.ToUpper() == _baseCurrency.ToUpper())
        {
            return amount+" PLN"; // No conversion needed if the target currency is PLN
        }

        var requestUri = $"https://api.exchangeratesapi.io/latest?base={_baseCurrency}&symbols={targetCurrency.ToUpper()}";

        var response = await _httpClient.GetFromJsonAsync<ExchangeRateApiResponse>(requestUri);
        if (response != null && response.Rates != null && response.Rates.ContainsKey(targetCurrency.ToUpper()))
        {
            double rate = response.Rates[targetCurrency.ToUpper()];
            return amount * rate+" "+targetCurrency.ToUpper();
        }
        else
        {
            throw new Exception("Currency conversion failed.");
        }
    }

    public async Task<string> GetPredictedRevenue(int? softwareId, string? currency)
    {
        double predictedRevenue = 0;

        var contracts = _repository.Contracts.ToList();
        if (softwareId != null){
            contracts = _repository.Contracts
            .Where(c => c.SoftwareSystemId == softwareId)
            .ToList();
        }
        foreach (var contract in contracts)
        {
            // Get all payments for the current contract
            var payments = _repository.Payments
                .Where(p => p.ContractId == contract.IdContract && p.IsReturned==false)
                .ToList();

            // Calculate the sum of payment amounts
            double paymentSum = payments.Sum(p => p.Amount);
            if (paymentSum == contract.Amount)
            {
                predictedRevenue += contract.Amount;
            } else if (contract.EndDate>DateOnly.FromDateTime(DateTime.Today)){
                predictedRevenue += contract.Amount;
            }
        }

        return await ConvertCurrency(predictedRevenue, currency);

    }

    public async Task<string> GetRevenue(int? softwareId, string? currency)
    {
        double revenue = 0;

        var contracts = _repository.Contracts.ToList();
        if (softwareId != null){
            contracts = _repository.Contracts
            .Where(c => c.SoftwareSystemId == softwareId)
            .ToList();
        }
        foreach (var contract in contracts)
        {
            // Get all payments for the current contract
            var payments = _repository.Payments
                .Where(p => p.ContractId == contract.IdContract && p.IsReturned==false)
                .ToList();

            // Calculate the sum of payment amounts
            double paymentSum = payments.Sum(p => p.Amount);

            // Check if the payment sum equals the contract amount
            if (paymentSum == contract.Amount)
            {
                revenue += contract.Amount;
            }
        }

        return  await ConvertCurrency(revenue, currency);
    }

    public class ExchangeRateApiResponse
    {
        public Dictionary<string, double> Rates { get; set; }
    }
}