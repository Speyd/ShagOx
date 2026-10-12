using DotNetEnv;
using ShagOxServer.Api.Common;
using ShagOxServer.SharedKernel.Configuration;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadJwtEnvironment(
        IHostApplicationBuilder builder)
    {
        var envPath = EnvironmentFileLocator.Find(EnvExtension);

        Env.Load(envPath);

        builder.Configuration["Jwt:Key"] =
            GetRequiredEnvironmentVariable("JWT_SECRET");
    }
}
