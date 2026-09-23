using Serilog;

namespace ShagOxServer.Api.DependencyInjection;
public static class SerilogExstension
{
    public static IServiceCollection AddSerilogConfiguration(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddSerilog(
         (services, loggerConfiguration) => loggerConfiguration
             .ReadFrom.Configuration(config)
             .ReadFrom.Services(services)
             .Enrich.FromLogContext());

        return services;
    }
}