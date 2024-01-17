using Application.Dtos.Shipments;

namespace Application.Interfaces.Services;

    public interface IShipmentsServiceAsync
    {
        public Task<IEnumerable<GetShipmentDto>> GetAllShipmentsAsync();
        public Task<GetShipmentDto?> GetShipmentByIdAsync(uint shipmentId);
        public Task<GetShipmentDto> AddShipmentToCustomerAsync(uint customerId, CreateShipmentDto shipment);
        public Task<GetShipmentDto> UpdateShipmentAsync(uint shipmentId, UpdateShipmentDto shipment);
        public Task DeleteShipmentAsync(uint shipmentId);
        public Task<IEnumerable<GetShipmentDto>> GetCustomerShipmentsAsync(uint customerId);
    }
