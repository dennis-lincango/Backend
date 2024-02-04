using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Customers;
using Infrastructure.Services.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Database context
        services.AddDbContext<EntityFrameworkDbContext>
        (
            option => option.UseSqlite(configuration.GetConnectionString("SqliteDevelopmentConnection"))
        );

        //Repositories
        services.AddScoped<ICustomersRepositoryAsync, EntityFrameworkCustomersRepositoryAsync>();
        services.AddScoped<IShipmentsRepositoryAsync, EntityFrameworkShipmentsRepositoryAsync>();
        services.AddScoped<IUsersRepositoryAsync, EntityFrameworkUsersRepositoryAsync>();
        
        //Services
        services.AddScoped<ICustomersServiceAsync, DefaultCustomersServiceAsync>();
        services.AddScoped<IShipmentsServiceAsync, DefaultShipmentsServiceAsync>();
        services.AddScoped<IAuthServiceAsync, DefaultAuthServiceAsync>();

        return services;
    }
}
