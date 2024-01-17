using Application.Common.Exceptions;
using Application.Dtos.Shipments;
using Application.Dtos.Shipments.Extensions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Infrastructure.Services.Shipments;

public class DefaultShipmentsServiceAsync
(
    ICustomersRepositoryAsync customersRepository,
    IShipmentsRepositoryAsync shipmentRepository
) : IShipmentsServiceAsync
{
    public async Task<GetShipmentDto> AddShipmentToCustomerAsync(uint customerId, CreateShipmentDto shipment)
    {
        Customer? customer = await customersRepository.GetByIdAsync(customerId)
            ?? throw new NotFoundException(nameof(Customer), customerId);
        
        Shipment newShipment = shipment.ToShipment(customer);
        return (await shipmentRepository.AddAsync(newShipment)).ToGetShipmentDto();
    }

    public async Task DeleteShipmentAsync(uint shipmentId)
    {
        await shipmentRepository.DeleteAsync(shipmentId);
    }

    public async Task<IEnumerable<GetShipmentDto>> GetAllShipmentsAsync()
    {
        return await shipmentRepository.GetAllAsync(
            selector: shipment => shipment.ToGetShipmentDto()
        );
    }

    public async Task<GetShipmentDto?> GetShipmentByIdAsync(uint shipmentId)
    {
        return await shipmentRepository.GetByIdAsync(
            shipmentId,
            selector: shipment => shipment.ToGetShipmentDto()
        );
    }

    public async Task<GetShipmentDto> UpdateShipmentAsync(uint shipmentId, UpdateShipmentDto shipment)
    {
        Shipment? shipmentToUpdate = await shipmentRepository.GetByIdAsync(shipmentId) 
            ?? throw new NotFoundException(nameof(Shipment), shipmentId);
        
        shipmentToUpdate = shipmentToUpdate.UpdateShipmentValues(shipment);
        return (await shipmentRepository.UpdateAsync(shipmentToUpdate)).ToGetShipmentDto();
    }

    public async Task<IEnumerable<GetShipmentDto>> GetCustomerShipmentsAsync(uint customerId)
    {
        return await shipmentRepository.GetAsync(
            filter: shipment => shipment.CustomerId == customerId,
            selector: shipment => shipment.ToGetShipmentDto()
        );
    }
}
