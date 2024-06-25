namespace Services.Interfaces;
using DTOs;
using Models;
public interface ISubscriptionService
{
    Task<bool> UpdateSubscription(int IdClient, int IdSubscription, int RenewalPeriod);
}