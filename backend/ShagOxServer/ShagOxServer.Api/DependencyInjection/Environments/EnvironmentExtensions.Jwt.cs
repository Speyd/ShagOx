using DotNetEnv;
using ShagOxServer.Api.Common;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadJwtEnvironment(
        IHostApplicationBuilder builder)
    {
        var redisEnvPath = Path.Combine(
            ProjectPath.Root,
            EnvExtension);

        Env.Load(redisEnvPath);

        builder.Configuration["Jwt:Key"] =
            GetRequiredEnvironmentVariable("JWT_SECRET");
    }
}
