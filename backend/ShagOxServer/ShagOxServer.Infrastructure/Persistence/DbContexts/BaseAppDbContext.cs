using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public abstract partial class BaseAppDbContext : DbContext
{
    protected BaseAppDbContext(
        DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BaseAppDbContext).Assembly);

        //modelBuilder.Entity<User>()
        //    .Property<uint>("xmin")
        //    .IsRowVersion();
    }
}
