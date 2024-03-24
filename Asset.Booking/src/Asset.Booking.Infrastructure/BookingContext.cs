namespace Asset.Booking.Infrastructure;

using Domain.AssetSchedule;
using Domain.Client;
using EntityConfigurations.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedKernel;
using SharedKernel.Abstractions;

/// <remarks>
/// Add migrations using the following command inside the 'Asset.Booking.Infrastructure' project directory:
/// -> dotnet ef migrations add --startup-project ../Asset.Booking.API --context BookingContext  --output-dir Migrations/Booking [migration-name]
///
/// Apply migrations:
/// -> dotnet ef database update --startup-project ../Asset.Booking.API --context BookingContext
/// </remarks>
public class BookingContext(DbContextOptions<BookingContext> options, IConfiguration configuration)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<AssetSchedule> AssetSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DbConnectionStrings.AssetBookingDb));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("booking");
        modelBuilder.ApplyConfiguration(new AssetScheduleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ClientEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationEntityConfiguration());
    }
}
