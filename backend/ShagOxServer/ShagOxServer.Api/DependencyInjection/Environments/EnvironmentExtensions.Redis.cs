using DotNetEnv;
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
            GetRequiredEnvironmentVariable("REDIS_DATA_USER");

        builder.Configuration["Redis:Password"] =
            GetRequiredEnvironmentVariable("REDIS_DATA_PASSWORD");

        builder.Configuration["Redis:Endpoints"] =
            $"127.0.0.1:{GetRequiredEnvironmentVariable("REDIS_DATA_PORT")}";


        builder.Configuration["UserRedis:User"] =
            GetRequiredEnvironmentVariable("REDIS_USER");

        builder.Configuration["UserRedis:Password"] =
            GetRequiredEnvironmentVariable("REDIS_USER_PASSWORD");

        builder.Configuration["UserRedis:Endpoints"] =
            $"127.0.0.1:{GetRequiredEnvironmentVariable("REDIS_USER_PORT")}";
    }
}
