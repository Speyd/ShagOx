using Microsoft.EntityFrameworkCore;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public abstract partial class BaseAppDbContext : DbContext
{
    protected BaseAppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BaseAppDbContext).Assembly);
    }
}