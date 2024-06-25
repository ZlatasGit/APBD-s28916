namespace PROJECT.Services.Interfaces;
using DTOs;

public interface IRevenueService
{
    // 6. get revenue 
    Task<string> GetRevenue(int? softwareId, string? currency);

    // 7. get predicted revenue
    Task<string> GetPredictedRevenue(int? softwareId, string? currency);

}