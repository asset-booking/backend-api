namespace Asset.Booking.Infrastructure;

using EntityConfigurations.Management;
using Management.Domain.Asset;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedKernel;
using SharedKernel.Abstractions;

/// <remarks>
/// Add migrations using the following command inside the 'Asset.Booking.Infrastructure' project directory:
/// -> dotnet ef migrations add --startup-project ../Asset.Booking.API --context ManagementContext --output-dir Migrations/Management [migration-name]
///
/// Apply migrations:
/// -> dotnet ef database update --startup-project ../Asset.Booking.API --context ManagementContext
/// </remarks>
public class ManagementContext(DbContextOptions<ManagementContext> options, IConfiguration configuration)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DbConnectionStrings.AssetBookingDb));
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("management");
        modelBuilder.ApplyConfiguration(new AssetEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
    }
}