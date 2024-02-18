using Application.Dtos.Customers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/customers")]
public class CustomersController(
    ICustomersServiceAsync customersService,
    ILogger<CustomersController> logger
    ) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrative, Operational")]
    public async Task<ActionResult<IEnumerable<GetCustomerDto>>> Get()
    {
        logger.LogInformation($"Customers found: A successful get all customers request from {this.GetAuthTokenUser() } was made.");
        return Ok(await customersService.GetAllCustomersAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrative, Operational")]
    public async Task<ActionResult<GetCustomerDto>> Get(uint id)
    {
        GetCustomerDto? customer = await customersService.GetCustomerByIdAsync(id);
        string? username = this.GetAuthTokenUser();

        if (customer == null)
        {
            logger.LogInformation($"Customer not found: A failed get customer with id {id} request from {username} was made.");
            return NotFound();
        }
        else
        {
            logger.LogInformation($"Customer found: A sucessful get customer with id {id} request from {username} was made.");
            return Ok(customer);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Administrative")]
    public async Task<IActionResult> Post([FromBody] CreateCustomerDto customer)
    {
        var createdCustomer = await customersService.AddCustomerAsync(customer);
        logger.LogInformation($"Customer created: A sucessful create customer with id {createdCustomer.Id} request from {this.GetAuthTokenUser()} was made.");
        return CreatedAtAction(nameof(Get), new { id = createdCustomer.Id }, createdCustomer);
    }
}
