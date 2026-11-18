using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using ShagOxServer.Application.Common.Options.Db.Postgres;
using ShagOxServer.SharedKernel.Configuration;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
public sealed class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(
        string[] args)
    {
        var envPath = EnvironmentFileLocator.Find();
        Env.Load(envPath);

        PostgresOptions option =
            PostgresLoaderOptions.Load();

        var connectionString =
            new NpgsqlConnectionStringBuilder
            {
                Host = option.PrimaryHost,
                Port = option.PrimaryPort,
                Database = option.Database,
                Username = option.User,
                Password = option.Password,
                SslMode = SslMode.Disable
            }.ConnectionString;

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}
