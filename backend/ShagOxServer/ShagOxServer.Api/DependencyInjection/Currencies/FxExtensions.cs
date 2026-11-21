using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Currencies.Fx;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies.FxApi;
using ShagOxServer.Infrastructure.ExternalServices.Currencies.FxApi;

namespace ShagOxServer.Api.DependencyInjection.Currencies;
public static class FxExtensions
{
    public static IServiceCollection AddFx(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.Configure<FxApiOptions>(
            configuration.GetSection("FxApi"));

        services.AddHttpClient<IFxApiService, FxApiService>(
        (sp, client) =>
        {
            var options = sp
                .GetRequiredService<IOptions<FxApiOptions>>()
                .Value;

            client.BaseAddress = new Uri(options.BaseUrl);

            client.DefaultRequestHeaders.Add(
                "apikey",
                options.ApiKey);
        });

        return services;
    }
}
