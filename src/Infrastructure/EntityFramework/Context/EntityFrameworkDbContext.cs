using Azure.Identity;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context;

public class EntityFrameworkDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<User> Users => Set<User>();

    public EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> options): base(options)
    {
        SqlColumnEncryptionAzureKeyVaultProvider azureKeyVaultProvider = new(new DefaultAzureCredential());
        Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers = new(){
            [SqlColumnEncryptionAzureKeyVaultProvider.ProviderName] = azureKeyVaultProvider
        };
        SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(EntityFrameworkDbContext).Assembly);
        ConfigureTransformations(builder);
        ConfigureForeignKeys(builder);
    }

    private static void ConfigureTransformations(ModelBuilder builder)
    {
        builder.Entity<Shipment>()
            .Property(x => x.CargoType)
            .HasConversion<string>();

        builder.Entity<User>()
            .Property(x => x.UserType)
            .HasConversion<string>();
    }

    private static void ConfigureForeignKeys(ModelBuilder builder)
    {
        builder.Entity<Shipment>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.Shipments)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();
    }
}
