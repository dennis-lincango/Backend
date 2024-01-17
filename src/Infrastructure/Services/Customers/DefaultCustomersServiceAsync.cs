using Application.Dtos.Customers;
using Application.Dtos.Customers.Extensions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Infrastructure.Services.Customers;

public class DefaultCustomersServiceAsync(ICustomersRepositoryAsync customersRepositoryAsync)
: ICustomersServiceAsync
{
    public async Task<GetCustomerDto> AddCustomerAsync(CreateCustomerDto customerDto)
    {
        Customer customer = customerDto.ToCustomer();
        return (await customersRepositoryAsync.AddAsync(customer)).ToGetCustomerDto();
    }

    public async Task<IEnumerable<GetCustomerDto>> GetAllCustomersAsync()
    {
        return await customersRepositoryAsync.GetAllAsync(
            selector: customer => customer.ToGetCustomerDto()
        );
    }

    public async Task<GetCustomerDto?> GetCustomerByIdAsync(uint id)
    {
        return await customersRepositoryAsync.GetByIdAsync(
            id: id,
            selector: customer => customer.ToGetCustomerDto()
        );
    }
}
