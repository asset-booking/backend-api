namespace Asset.Booking.Infrastructure.Factories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class BookingContextFactory : IDesignTimeDbContextFactory<BookingContext>
{
    public BookingContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<BookingContext>();
        return new BookingContext(optionsBuilder.Options, configuration);
    }
}