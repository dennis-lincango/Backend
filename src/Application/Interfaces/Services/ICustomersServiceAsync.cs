using Application.Dtos.Customers;

namespace Application.Interfaces.Services;

public interface ICustomersServiceAsync
{
    public Task<GetCustomerDto> AddCustomerAsync(CreateCustomerDto customerDto);
    public Task<GetCustomerDto?> GetCustomerByIdAsync(uint id);
    public Task<IEnumerable<GetCustomerDto>> GetAllCustomersAsync();
}
