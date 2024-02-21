using Azure.Identity;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.EntityFramework.Context
{
    /// <summary>
    /// Represents the Entity Framework database context for the application.
    /// </summary>
    public class EntityFrameworkDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the Customers DbSet.
        /// </summary>
        public DbSet<Customer> Customers => Set<Customer>();

        /// <summary>
        /// Gets or sets the Shipments DbSet.
        /// </summary>
        public DbSet<Shipment> Shipments => Set<Shipment>();

        /// <summary>
        /// Gets or sets the Users DbSet.
        /// </summary>
        public DbSet<User> Users => Set<User>();

        private static SqlColumnEncryptionAzureKeyVaultProvider? _azureKeyVaultProvider = null;


        public EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> options) : base(options)
        {
            // Check if the Azure Key Vault provider is not initialized
            if (_azureKeyVaultProvider == null)
            {
                // Initialize the Azure Key Vault provider with DefaultAzureCredential
                _azureKeyVaultProvider = new SqlColumnEncryptionAzureKeyVaultProvider(new DefaultAzureCredential());

                // Create a dictionary of SQL column encryption key store providers
                Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers = new()
                {
                    [SqlColumnEncryptionAzureKeyVaultProvider.ProviderName] = _azureKeyVaultProvider
                };

                // Register the column encryption key store providers with SqlConnection
                SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
            }
        }

        /// <summary>
        /// Configures the model for the database context.
        /// </summary>
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
}
