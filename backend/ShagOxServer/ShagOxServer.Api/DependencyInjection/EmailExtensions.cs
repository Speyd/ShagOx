using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Services.Verifications.Sending;

namespace ShagOxServer.Api.DependencyInjection;
public static class EmailExtensions
{
    public static IServiceCollection AddDevelopmentEmailFallback(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        if (!environment.IsDevelopment() ||
            !string.IsNullOrWhiteSpace(configuration["Email:SmtpHost"]))
        {
            return services;
        }

        services.AddScoped<IEmailService,
            DevelopmentEmailService>();

        return services;
    }
}
