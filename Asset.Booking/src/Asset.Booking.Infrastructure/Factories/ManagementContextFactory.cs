namespace Asset.Booking.Infrastructure.Factories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class ManagementContextFactory : IDesignTimeDbContextFactory<ManagementContext>
{
    public ManagementContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<ManagementContext>();
        return new ManagementContext(optionsBuilder.Options, configuration);
    }
}