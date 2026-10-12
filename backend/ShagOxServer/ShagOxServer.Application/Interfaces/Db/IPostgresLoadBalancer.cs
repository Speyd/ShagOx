using ShagOxServer.Domain.Entities.Db;

namespace ShagOxServer.Application.Interfaces.Db;
public interface IPostgresLoadBalancer
{
    Replica? SelectReplica();
}
