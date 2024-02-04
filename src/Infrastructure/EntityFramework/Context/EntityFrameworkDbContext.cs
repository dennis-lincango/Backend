using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context;

public class EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<User> Users => Set<User>();

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
