using Serilog;
using ShagOxServer.Api.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Base;
using ShagOxServer.Infrastructure.DependencyInjections.Base;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    #region Services

    builder.Services
        .AddLanguageProvider()
        .AddSerilogConfiguration(builder.Configuration)
        .AddDatabase(builder.Configuration)
        .AddJWT(builder.Configuration)
        .AddCloudinary(builder.Configuration)
        .AddSettingsConfiguration(builder.Configuration)
        .AddFrontendPolicy()
        .AddControllersWithJson()
        .AddSwaggerDocumentation()
        .AddApplication()
        .AddInfrastructure()
        .AddHttpContextAccessor();

    #endregion

    #region Pipeline

    var app = builder.Build();

    app.ConfigurePipeline();

    #endregion

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}