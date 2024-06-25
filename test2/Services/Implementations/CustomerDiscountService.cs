namespace Services.Implementations;

using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;

public class CustomerDiscountService : ICustomerDiscountService
{
    private readonly AppDbContext _context;

    public CustomerDiscountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDiscountsDTO> RetrieveActiveDiscountsForCustomer(int id)
    {
        var customer = await _context.Clients.FindAsync(id);
        if (customer == null)
        {
            return null;
        }

        var customerDiscounts = await _context.Discounts
            .Where(cd => cd.ClientId == id && cd.DateTo > DateOnly.FromDateTime(DateTime.Today))
            .ToListAsync();
        var discountDTOs = new List<DiscountDTO>();

        foreach (var cd in customerDiscounts)
        {
            discountDTOs.Add(MapToDTO(cd));
        }
        return new CustomerDiscountsDTO
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            Discounts = discountDTOs
        };
    }

    private DiscountDTO MapToDTO(Discount discount)
    {
        return new DiscountDTO
        {   
            IdDiscount = discount.IdDiscount,
            Description = generateDescription(),
            Value = discount.Value+"%",
            EndDate = discount.DateTo
        };
    }

    private string generateDescription(){
        List<string> descriptions = new List<string> {
            "Discount for loyal customers",
            "Discount for new customers",
            "New year discount",
            "Christmas discount",
            "Black Friday discount",
            "Cyber Monday discount"
        };
        Random random = new Random();
        return descriptions[random.Next(0, descriptions.Count)];
    }
}