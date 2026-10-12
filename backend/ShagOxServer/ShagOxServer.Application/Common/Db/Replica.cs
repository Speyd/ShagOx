namespace ShagOxServer.Application.Common.Db;
public sealed class Replica
{
    public string ConnectionString { get; }

    public bool IsHealthy { get; set; } = true;

    public float Cpu { get; set; }

    public int ActiveConnections { get; set; }

    public DateTime LastHealthCheck { get; set; }


    public Replica(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
