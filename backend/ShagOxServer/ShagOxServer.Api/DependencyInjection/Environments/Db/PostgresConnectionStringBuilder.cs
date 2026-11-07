using Npgsql;
using System.Globalization;

namespace ShagOxServer.Api.DependencyInjection.Environments.Db;
internal static class PostgresConnectionStringBuilder
{
    public static string Build(
        string user,
        string password,
        string database,
        string host,
        string port)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = int.Parse(port, CultureInfo.InvariantCulture),
            Database = database,
            Username = user,
            Password = password,
            SslMode = SslMode.Disable,
        };

        return builder.ConnectionString;
    }
}
