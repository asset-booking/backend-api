namespace Asset.Booking.Infrastructure;

using Domain.AssetSchedule.Abstractions;
using Domain.Client.Abstractions;
using Management.Domain.Asset.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddAssetBookingInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAssetScheduleRepository, AssetScheduleRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddDbContext<ManagementContext>();
        services.AddDbContext<BookingContext>();
        
        return services;
    }
}