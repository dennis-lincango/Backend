using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.EntityFramework.Common.Repositories;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories;

public class EntityFrameworkShipmentsRepositoryAsync(EntityFrameworkDbContext context)
        : EntityFrameworkGenericRepositoryAsync<Shipment, uint>(context, context.Shipments), IShipmentsRepositoryAsync
{
}
