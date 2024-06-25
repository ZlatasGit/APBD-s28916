namespace Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTOs;
using Data;
using Services.Interfaces;
using Services.Implementations;

[ApiController]
[Route("[controller]")]
public class MyAppContorller : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly ICustomerDiscountService _customerDiscountService;

    public MyAppContorller(ISubscriptionService subscriptionService, ICustomerDiscountService customerDiscountService)
    {
        _subscriptionService = subscriptionService;
        _customerDiscountService = customerDiscountService;
    }

    [HttpGet]
    [Route("/customer/{idCustomer}/active-discounts")]
    public async Task<IActionResult> GetDiscounts(int idCustomer)
    {
        var discounts = await _customerDiscountService.RetrieveActiveDiscountsForCustomer(idCustomer);
        if (discounts == null)
        {
            return NotFound();
        }
        return Ok(discounts);
    }

    [HttpPost]
    [Route("/customer/{idClient}/subscription/{idSubscription}/renewal-period")]
    public async Task<IActionResult> UpdateSubscription(int idClient, int idSubscription, [FromBody] int renewalPeriod)
    {
        var success = await _subscriptionService.UpdateSubscription(idClient, idSubscription, renewalPeriod);
        if (!success)
        {
            return NotFound();
        }
        return Ok();
    }
}