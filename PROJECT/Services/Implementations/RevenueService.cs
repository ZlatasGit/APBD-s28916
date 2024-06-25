using System.Text.Json;
using System.Text.Json.Serialization;
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
        var reply = await _httpClient.GetAsync($"https://v6.exchangerate-api.com/v6/5652e416452bb267d2f7cca3/pair/PLN/{targetCurrency.ToUpper()}");
        reply.EnsureSuccessStatusCode();

        var content = await reply.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<Response>(content);
        return Convert.ToDouble(response.ConversionRate)*amount+" "+targetCurrency.ToUpper();
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
            var payments = _repository.Payments
                .Where(p => p.ContractId == contract.IdContract && p.IsReturned==false)
                .ToList();

            double paymentSum = payments.Sum(p => p.Amount);

            if (paymentSum == contract.Amount)
            {
                revenue += contract.Amount;
            } else if (contract.EndDate>DateOnly.FromDateTime(DateTime.Today)){
                revenue += paymentSum;
            }
        }

        return  await ConvertCurrency(revenue, currency);
    }
    public class Response
    {
        [JsonPropertyName("result")] public string Result { get; set; }

        [JsonPropertyName("documentation")] public string Documentation { get; set; }

        [JsonPropertyName("terms_of_use")] public string TermsOfUse { get; set; }

        [JsonPropertyName("time_last_update_unix")]
        public long TimeLastUpdateUnix { get; set; }

        [JsonPropertyName("time_last_update_utc")]
        public string TimeLastUpdateUtc { get; set; }

        [JsonPropertyName("time_next_update_unix")]
        public long TimeNextUpdateUnix { get; set; }

        [JsonPropertyName("time_next_update_utc")]
        public string TimeNextUpdateUtc { get; set; }

        [JsonPropertyName("base_code")] public string BaseCode { get; set; }

        [JsonPropertyName("target_code")] public string TargetCode { get; set; }

        [JsonPropertyName("conversion_rate")] public decimal ConversionRate { get; set; }
    }
    
}