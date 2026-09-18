namespace ShagOxServer.Api.DependencyInjection;

public static class CorsExtensions
{
    public static IServiceCollection AddFrontendPolicy(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Frontend", policy =>
            {
                policy
                    .WithOrigins(
                    "http://localhost:5173",
                    "https://dev.dmarketly.com", 
                    "https://www.dmarketly.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}