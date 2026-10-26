using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public static class GlobalQueryFiltersExtensions
{
    public static void ConfigureGlobalQueryFilters(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseEntity>()
            .HasQueryFilter(x => x.EntityStatus == EntityStatus.Active);
    }
}
