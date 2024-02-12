using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Configurations;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.MemoryCache.Repositories;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Customers;
using Infrastructure.Services.FailedAttemptsReset;
using Infrastructure.Services.Hash;
using Infrastructure.Services.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Database context
        services.AddDbContext<EntityFrameworkDbContext>
        (
            option => option.UseSqlServer(configuration.GetConnectionString("SqlServerDevelopmentConnection"))
        );

        // Add memory cache
        services.AddMemoryCache(
            options =>
            {
                options.SizeLimit = 1024 * 1024 * 100;
                options.CompactionPercentage = 0.2;
            }
        );

        //Configurations for Infrastructure
        services.Configure<JwtAccessTokenConfigurations>(configuration.GetSection("JwtAccessToken"));

        //Repositories
        services.AddScoped<ICustomersRepositoryAsync, EntityFrameworkCustomersRepositoryAsync>();
        services.AddScoped<IShipmentsRepositoryAsync, EntityFrameworkShipmentsRepositoryAsync>();
        services.AddScoped<IUsersRepositoryAsync, EntityFrameworkUsersRepositoryAsync>();
        services.AddSingleton<IWhitelistRepository, MemoryCacheWhitelistRepository>();
        services.AddSingleton<IBlacklistRepository, MemoryCacheBlacklistRepository>();

        //Services
        services.AddScoped<ICustomersServiceAsync, DefaultCustomersServiceAsync>();
        services.AddScoped<IShipmentsServiceAsync, DefaultShipmentsServiceAsync>();
        services.AddScoped<IAuthServiceAsync, JwtAuthServiceAsync>();
        services.AddSingleton<IHashService, BcryptHashService>();
        services.AddHostedService<BackgroundFailedAttemptsResetServiceAsync>();

        return services;
    }
}
