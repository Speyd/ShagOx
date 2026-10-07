using DotNetEnv;
using ShagOxServer.Api.Common;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadRedisEnvironment(
        IHostApplicationBuilder builder)
    {
        var redisEnvPath = Path.Combine(
            ProjectPath.Root,
            EnvExtension);

        Env.Load(redisEnvPath);

        builder.Configuration["Redis:User"] =
            GetRequiredEnvironmentVariable("REDIS_USER");

        builder.Configuration["Redis:Password"] =
            GetRequiredEnvironmentVariable("REDIS_PASSWORD");
    }
}