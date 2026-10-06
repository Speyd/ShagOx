using DotNetEnv;
using ShagOxServer.Api.Common;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadRedisEnvironment(
        IHostApplicationBuilder builder)
    {
        var redisRoot = Path.Combine(
            ProjectPath.Root,
            RedisDataPath);

        var redisEnvPath = Path.Combine(
            redisRoot,
            EnvExtension);

        Env.Load(redisEnvPath);

        builder.Configuration["Redis:User"] =
            GetRequiredEnvironmentVariable("REDIS_USER");

        builder.Configuration["Redis:Password"] =
            GetRequiredEnvironmentVariable("REDIS_PASSWORD");
    }
}