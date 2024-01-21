using Application.Dtos.Customers.Extensions;
using Domain.Entities;

namespace Application.Dtos.Shipments.Extensions;

public static class ShipmentDtoMappingExtensions
{
    public static GetShipmentDto ToGetShipmentDto(this Shipment shipment)
    {
        return new GetShipmentDto()
        {
            Id = shipment.Id,
            Description = shipment.Description,
            Weight = shipment.Weight,
            Length = shipment.Length,
            Width = shipment.Width,
            Height = shipment.Height,
            SourceAddress = shipment.SourceAddress,
            SourceCity = shipment.SourceCity,
            DestinationAddress = shipment.DestinationAddress,
            DestinationCity = shipment.DestinationCity,
            CargoType = shipment.CargoType,
            Customer = shipment.Customer.ToGetCustomerDto()
        };
    }

    public static Shipment ToShipment(this CreateShipmentDto shipment, Customer customer)
    {
        return new Shipment()
        {
            Description = shipment.Description,
            Weight = shipment.Weight,
            Length = shipment.Length,
            Width = shipment.Width,
            Height = shipment.Height,
            SourceAddress = shipment.SourceAddress,
            SourceCity = shipment.SourceCity,
            DestinationAddress = shipment.DestinationAddress,
            DestinationCity = shipment.DestinationCity,
            CargoType = shipment.CargoType,
            CustomerId = customer.Id
        };
    }

    public static Shipment UpdateShipmentValues(this Shipment shipment, UpdateShipmentDto shipmentDto)
    {
        shipment.Description = shipmentDto.Description;
        shipment.Weight = shipmentDto.Weight;
        shipment.Length = shipmentDto.Length;
        shipment.Width = shipmentDto.Width;
        shipment.Height = shipmentDto.Height;
        shipment.SourceAddress = shipmentDto.SourceAddress;
        shipment.SourceCity = shipmentDto.SourceCity;
        shipment.DestinationAddress = shipmentDto.DestinationAddress;
        shipment.DestinationCity = shipmentDto.DestinationCity;
        shipment.CargoType = shipmentDto.CargoType;

        return shipment;
    }
}
