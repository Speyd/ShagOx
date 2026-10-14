namespace ShagOxServer.Domain.Entities.Db;
public sealed class Replica
{
    public string ConnectionString { get; }

    public string ContainerName { get; }

    public bool IsHealthy { get; set; } = true;

    public float Cpu { get; set; }

    public int ActiveConnections { get; set; }

    public DateTime LastHealthCheck { get; set; }


    public Replica(
        string connectionString,
        string сontainerName)
    {
        ConnectionString = connectionString;
        ContainerName = сontainerName;
    }
}
