using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Db;
using ShagOxServer.Application.Interfaces.Db;
using ShagOxServer.Domain.Entities.Db;

namespace ShagOxServer.Infrastructure.Persistence.Db;
public class PostgresLoadBalancer
    : IPostgresLoadBalancer
{
    private readonly IReadOnlyList<Replica> _replicas;
    private readonly LoadBalancerOptions _setting;


    public PostgresLoadBalancer(
        IReadOnlyList<Replica> replicas,
        IOptions<LoadBalancerOptions> setting)
    {
        _replicas = replicas;
        _setting = setting.Value;
    }


    public Replica? SelectReplica()
    {
        var queue = new PriorityQueue<Replica, float>();
        var now = DateTime.UtcNow;

        foreach (Replica replica in _replicas)
        {
            if (!replica.IsHealthy)
                continue;

            if (replica.LastHealthCheck == default)
                continue;

            if (now - replica.LastHealthCheck > _setting.MaxHealthAge)
                continue;

            queue.Enqueue(replica, replica.Cpu);
        }

        if (queue.TryDequeue(out var selected, out _))
            return selected;

        return null;
    }
}
