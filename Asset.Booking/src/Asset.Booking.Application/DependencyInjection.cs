namespace Asset.Booking.Application;

using Abstractions.Configurations;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAssetBookingApplication(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
        services.AddAutomapper();

        return services;
    }

    private static void AddAutomapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddMaps(AssemblyReference.Assembly));

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }
}
