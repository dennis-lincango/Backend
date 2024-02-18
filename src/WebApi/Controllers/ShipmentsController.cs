using Application.Dtos.Shipments;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using WebApi.Extensions;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/shipments")]
public class ShipmentsController(
    IShipmentsServiceAsync shipmentService,
    ILogger<ShipmentsController> logger
    ) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrative, Operational")]
    public async Task<ActionResult<IEnumerable<GetShipmentDto>>> Get()
    {
        string? username = this.GetAuthTokenUser();
        logger.LogInformation($"Shipments found: A successful get all shipments request from {username} was made.");
        return Ok(await shipmentService.GetAllShipmentsAsync());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrative, Operational")]
    public async Task<ActionResult<GetShipmentDto>> Get(uint id)
    {
        string? username = this.GetAuthTokenUser();
        GetShipmentDto? shipment = await shipmentService.GetShipmentByIdAsync(id);
        if (shipment == null)
        {
            logger.LogInformation($"Shipment not found: A failed get shipment with id {id} request from {username} was made.");
            return NotFound();
        }
        else
        {
            logger.LogInformation($"Shipment found: A sucessful get shipment with id {id} request from {username} was made.");
            return Ok(shipment);
        }
    }

    [HttpPost("~/api/customers/{customerId}/shipments")]
    [Authorize(Roles = "Administrative")]
    public async Task<ActionResult<GetShipmentDto>> Post(uint customerId, [FromBody] CreateShipmentDto shipment)
    {
        string? username = this.GetAuthTokenUser();
        try
        {
            GetShipmentDto createdShipment = await shipmentService.AddShipmentToCustomerAsync(customerId, shipment);
            logger.LogInformation($"Shipment created: A successful create shipment with id {createdShipment.Id} request from {username} was made.");
            return CreatedAtAction(nameof(Get), new { id = createdShipment.Id }, createdShipment);
        }
        catch (NotFoundException)
        {
            logger.LogInformation($"Shipment not created: A failed create shipment request from {username} was made.");
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrative")]
    public async Task<ActionResult<GetShipmentDto>> Put(uint id, [FromBody] UpdateShipmentDto shipment)
    {
        string? username = this.GetAuthTokenUser();
        try
        {
            GetShipmentDto updatedShipment = await shipmentService.UpdateShipmentAsync(id, shipment);
            logger.LogInformation($"Shipment updated: A successful update shipment with id {updatedShipment.Id} request from {username} was made.");
            return Ok(updatedShipment);
        }
        catch (NotFoundException)
        {
            logger.LogInformation($"Shipment not updated: A failed update shipment with id {id} request from {username} was made, because the shipment was not found.");
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrative")]
    public async Task<IActionResult> Delete(uint id)
    {
        string? username = this.GetAuthTokenUser();
        try
        {
            await shipmentService.DeleteShipmentAsync(id);
            logger.LogInformation($"Shipment deleted: A successful delete shipment with id {id} request from {username} was made.");
            return Ok();
        }
        catch (NotFoundException)
        {
            logger.LogInformation($"Shipment not deleted: A failed delete shipment with id {id} request from {username} was made, because the shipment was not found.");
            return NotFound();
        }
    }

    [HttpGet("~/api/customers/{customerId}/shipments")]
    [Authorize(Roles = "Administrative, Operational")]
    public async Task<ActionResult<IEnumerable<GetShipmentDto>>> GetCustomerShipments(uint customerId)
    {
        logger.LogInformation($"Shipments found: A successful get all shipments for customer with id {customerId} request from {this.GetAuthTokenUser()} was made.");  
        return Ok(await shipmentService.GetCustomerShipmentsAsync(customerId));
    }

}
