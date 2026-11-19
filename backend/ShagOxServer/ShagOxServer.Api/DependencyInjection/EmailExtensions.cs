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
        if (!environment.IsDevelopment())
            return services;

        if (string.IsNullOrWhiteSpace(configuration["Email:SmtpHost"]))
        {
            // No SMTP at all: only log the code.
            services.AddScoped<IEmailService,
                DevelopmentEmailService>();

            return services;
        }

        // SMTP configured: try it, and if it fails (blocked network etc.) log the code.
        services.AddScoped<EmailService>();
        services.AddScoped<IEmailService,
            SmtpWithDevelopmentFallbackEmailService>();

        return services;
    }
}
