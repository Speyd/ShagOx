using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using System.Linq.Expressions;

namespace ShagOxServer.Infrastructure.Persistence.DbContexts;
public static class GlobalQueryFiltersExtensions
{
    public static void ConfigureGlobalQueryFilters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType == typeof(BaseEntity))
                continue;

            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            if (entityType.BaseType != null &&
                entityType.BaseType.ClrType != typeof(BaseEntity))
                continue;


            var parameter = Expression.Parameter(entityType.ClrType, "x");
            var property = Expression.Property(parameter, nameof(BaseEntity.EntityStatus));
            var active = Expression.Constant(EntityStatus.Active);
            var body = Expression.Equal(property, active);
            var lambda = Expression.Lambda(body, parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }
}
