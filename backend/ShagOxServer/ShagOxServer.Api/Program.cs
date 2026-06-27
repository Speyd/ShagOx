using SchagoxServer.Api.DependencyInjection;
using ShagOxServer.Api.DependencyInjection;
using ShagOxServer.Application;
using ShagOxServer.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services
    .AddDatabase(builder.Configuration)
    .AddJWT(builder.Configuration)
    .AddFrontendPolicy()
    .AddControllersWithJson()
    .AddSwaggerDocumentation()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddHttpContextAccessor();

#endregion

var app = builder.Build();

#region Pipeline

app.ConfigurePipeline();

#endregion

app.Run();