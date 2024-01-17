using Domain.Entities;
using Application.Common.Interfaces.Repositories;

namespace Application.Interfaces.Repositories;

public interface IShipmentsRepositoryAsync : IGenericRepositoryAsync<Shipment, uint>
{

}
