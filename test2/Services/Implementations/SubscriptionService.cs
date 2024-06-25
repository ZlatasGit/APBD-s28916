namespace Services.Implementations;

using Services.Interfaces;
using Data;
using DTOs;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _context;

    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UpdateSubscription(int IdClient, int IdSubscription, int RenewalPeriod)
    {
        // check if client exists
        var client = await _context.Clients.FindAsync(IdClient);
        if (client == null)
        {
            return false;
        }
        // check if sale exists
        var sale = await _context.Sales
            .FirstOrDefaultAsync(s => s.SubscriptionId == IdSubscription && s.ClientId == IdClient);
        if (sale == null)
        {
            return false;
        }
        // check if subscription is active
        var subscription = _context.Subscriptions
            .Where(sub => sub.IdSubscription == IdSubscription && sub.EndTime > DateOnly.FromDateTime(DateTime.Today))
            .ToList().FirstOrDefault();
        if (subscription == null)
        {
            return false;
        }

        // check if payments are made
        var payments = await _context.Payments
            .Where(p => p.SubscriptionId == IdSubscription && p.Date <= subscription.EndTime)
            .ToListAsync();
        if (payments == null || payments.Count == 0)
        {
            return false;
        }
        var sum = payments.Sum(p => p.Value);

        if (sum < subscription.Price)
        {
            return false;
        } 
        subscription.RenewalPeriod = RenewalPeriod;
        await _context.SaveChangesAsync();
        return true;
    }

}