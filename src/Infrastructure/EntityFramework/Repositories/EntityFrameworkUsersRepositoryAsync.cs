using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.EntityFramework.Common.Repositories;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories;

public class EntityFrameworkUsersRepositoryAsync(EntityFrameworkDbContext context)
        : EntityFrameworkGenericRepositoryAsync<User, uint>(context, context.Users), IUsersRepositoryAsync
{

}
