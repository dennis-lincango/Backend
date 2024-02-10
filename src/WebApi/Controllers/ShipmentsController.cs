using Application.Dtos.Shipments;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/shipments")]
public class ShipmentsController(IShipmentsServiceAsync shipmentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetShipmentDto>>> Get()
    {
        return Ok(await shipmentService.GetAllShipmentsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetShipmentDto>> Get(uint id)
    {
        GetShipmentDto? shipment = await shipmentService.GetShipmentByIdAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(shipment);
        }
    }

    [HttpPost("~/api/customers/{customerId}/shipments")]
    public async Task<ActionResult<GetShipmentDto>> Post(uint customerId, [FromBody] CreateShipmentDto shipment)
    {
        try
        {
            GetShipmentDto createdShipment = await shipmentService.AddShipmentToCustomerAsync(customerId, shipment);
            return CreatedAtAction(nameof(Get), new { id = createdShipment.Id }, createdShipment);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetShipmentDto>> Put(uint id, [FromBody] UpdateShipmentDto shipment)
    {
        try
        {
            GetShipmentDto updatedShipment = await shipmentService.UpdateShipmentAsync(id, shipment);
            return Ok(updatedShipment);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        try
        {
            await shipmentService.DeleteShipmentAsync(id);
            return Ok();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("~/api/customers/{customerId}/shipments")]
    public async Task<ActionResult<IEnumerable<GetShipmentDto>>> GetCustomerShipments(uint customerId)
    {
        return Ok(await shipmentService.GetCustomerShipmentsAsync(customerId));
    }

}
