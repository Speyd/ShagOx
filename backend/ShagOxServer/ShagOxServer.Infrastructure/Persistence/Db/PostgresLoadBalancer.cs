using ShagOxServer.Application.Interfaces.Db;
using ShagOxServer.Domain.Entities.Db;

namespace ShagOxServer.Infrastructure.Persistence.Db;

public class PostgresLoadBalancer
    : IPostgresLoadBalancer
{
    private readonly IReadOnlyList<Replica> _replicas;


    public PostgresLoadBalancer(
        IReadOnlyList<Replica> replicas)
    {
        _replicas = replicas;
    }


    public Replica? SelectReplica()
    {
        PriorityQueue<Replica, float> replicas = new();

        foreach(Replica replica in _replicas)
        {
            if (replica.IsHealthy)
                continue;

            var lastCheck = DateTime.UtcNow - replica.LastHealthCheck;
            if (lastCheck < TimeSpan.FromMinutes(1))
                continue;

            replicas.Enqueue(replica, replica.Cpu);
        }

        return replicas.Dequeue();
    }
}
