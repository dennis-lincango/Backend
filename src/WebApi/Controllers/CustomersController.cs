using Application.Dtos.Customers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/customers")]
public class CustomersController(ICustomersServiceAsync customersService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrative, Operational")]
    public async Task<ActionResult<IEnumerable<GetCustomerDto>>> Get()
    {
        return Ok(await customersService.GetAllCustomersAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrative, Operational")]
    public async Task<ActionResult<GetCustomerDto>> Get(uint id)
    {
        GetCustomerDto? customer = await customersService.GetCustomerByIdAsync(id);

        if (customer == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(customer);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Administrative")]
    public async Task<IActionResult> Post([FromBody] CreateCustomerDto customer)
    {
        var createdCustomer = await customersService.AddCustomerAsync(customer);
        return CreatedAtAction(nameof(Get), new { id = createdCustomer.Id }, createdCustomer);
    }
}
