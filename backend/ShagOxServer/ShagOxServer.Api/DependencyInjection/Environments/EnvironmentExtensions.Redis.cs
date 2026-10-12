using DotNetEnv;
using ShagOxServer.Api.Common;
using ShagOxServer.SharedKernel.Configuration;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadRedisEnvironment(
        IHostApplicationBuilder builder)
    {
        var envPath = EnvironmentFileLocator.Find(EnvExtension);

        Env.Load(envPath);

        builder.Configuration["Redis:User"] =
            GetRequiredEnvironmentVariable("REDIS_USER");

        builder.Configuration["Redis:Password"] =
            GetRequiredEnvironmentVariable("REDIS_PASSWORD");
    }
}
