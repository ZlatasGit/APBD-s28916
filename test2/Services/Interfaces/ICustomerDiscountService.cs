namespace Services.Interfaces;
using DTOs;


public interface ICustomerDiscountService
{
    Task<CustomerDiscountsDTO> RetrieveActiveDiscountsForCustomer(int id);
}
