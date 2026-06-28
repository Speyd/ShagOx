using System.Text.Json.Serialization;

namespace ShagOxServer.Api.DependencyInjection;
public static class ControllerExtensions
{
    public static IServiceCollection AddControllersWithJson(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
            });

        return services;
    }
}