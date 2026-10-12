using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;
using ShagOxServer.Application.Common.Settings.Db.Postgres;
using ShagOxServer.SharedKernel.Configuration;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
public sealed class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var envPath = EnvironmentFileLocator.Find();
        Env.Load(envPath);

        PostgresSettings settings =
            PostgresSettingsLoader.Load();

        var connectionString =
            new NpgsqlConnectionStringBuilder
            {
                Host = settings.PrimaryHost,
                Port = settings.PrimaryPort,
                Database = settings.Database,
                Username = settings.User,
                Password = settings.Password,
                SslMode = SslMode.Disable
            }.ConnectionString;

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new AppDbContext(options);
    }
}
