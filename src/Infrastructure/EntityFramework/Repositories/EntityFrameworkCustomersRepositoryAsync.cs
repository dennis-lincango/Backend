using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.EntityFramework.Common.Repositories;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories;

public class EntityFrameworkCustomersRepositoryAsync(EntityFrameworkDbContext context)
        : EntityFrameworkGenericRepositoryAsync<Customer, uint>(context, context.Customers), ICustomersRepositoryAsync
{
}
