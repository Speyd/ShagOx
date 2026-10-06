using Microsoft.EntityFrameworkCore;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
public class ReplicaDbContext 
    : BaseAppDbContext
{
    public ReplicaDbContext(DbContextOptions<ReplicaDbContext> options)
        : base(options)
    {
    }
}