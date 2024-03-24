namespace Asset.Booking.Application;

using Abstractions.Configurations;
using Microsoft.Extensions.Configuration;
using SharedKernel;

public class ApplicationConfiguration (IConfiguration configuration) : IApplicationConfiguration
{
    public string SqlConnectionString { get; } = configuration.GetConnectionString(DbConnectionStrings.AssetBookingDb)
                                                 ?? throw new InvalidOperationException(DbConnectionStrings.AssetBookingDb);
}