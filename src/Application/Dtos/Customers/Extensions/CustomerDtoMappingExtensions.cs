using Domain.Entities;

namespace Application.Dtos.Customers.Extensions;

public static class CustomerDtoMappingExtensions
{
    public static GetCustomerDto ToGetCustomerDto(this Customer customer)
    {
        return new GetCustomerDto()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address
        };
    }

    public static Customer ToCustomer(this CreateCustomerDto customer)
    {
        return new Customer()
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address
        };
    }
}
