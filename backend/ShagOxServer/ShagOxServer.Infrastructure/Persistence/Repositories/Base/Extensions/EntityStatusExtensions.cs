using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using System.Linq.Expressions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
public static class EntityStatusExtensions
{
    public static IQueryable<TEntity> WithAvailableStatuses<TEntity>(
        this IQueryable<TEntity> query)
        where TEntity : BaseEntity
    {
        return query.Where(x =>
            x.EntityStatus == EntityStatus.Active ||
            x.EntityStatus == EntityStatus.Hidden);
    }

    public static IQueryable<TEntity> WithDeleted<TEntity>(
        this IQueryable<TEntity> query)
        where TEntity : BaseEntity
    {
        return query
            .Where(x => x.EntityStatus == EntityStatus.Deleted);
    }

    public static IQueryable<TEntity> WithAllStatuses<TEntity>(
        this IQueryable<TEntity> query)
        where TEntity : BaseEntity
    {
        return query;
    }

    public static IQueryable<TEntity> WithStatusFilter<TEntity>(
        this IQueryable<TEntity> query,
        EntityStatus? filter)
    where TEntity : BaseEntity
    {
        return filter switch
        {
            EntityStatus.Active =>
                query.Where(x => x.EntityStatus == EntityStatus.Active),

            EntityStatus.Hidden =>
                query.Where(x => x.EntityStatus == EntityStatus.Hidden),

            EntityStatus.Deleted =>
                query.Where(x => x.EntityStatus == EntityStatus.Deleted),

            EntityStatus.ActiveAndHidden =>
                query.Where(
                    x => x.EntityStatus == EntityStatus.Active ||
                    x.EntityStatus == EntityStatus.Hidden),

            EntityStatus.All => query,

            _ => query.Where(x => x.EntityStatus == EntityStatus.Active)
        };
    }

    public static Expression<Func<TEntity, bool>> GetStatusFilter<TEntity>(
        EntityStatus? filter)
        where TEntity : BaseEntity
    {
        return filter switch
        {
            EntityStatus.Active =>
                x => x.EntityStatus == EntityStatus.Active,

            EntityStatus.Hidden =>
                x => x.EntityStatus == EntityStatus.Hidden,

            EntityStatus.Deleted =>
                x => x.EntityStatus == EntityStatus.Deleted,

            EntityStatus.ActiveAndHidden =>
                x => x.EntityStatus == EntityStatus.Active ||
                     x.EntityStatus == EntityStatus.Hidden,

            EntityStatus.All => x => true,

            _ => x => x.EntityStatus == EntityStatus.Active
        };
    }
}
