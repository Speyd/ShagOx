using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using System.Linq.Expressions;

namespace ShagOxServer.Infrastructure.Persistence.Configurations.Extensions;
public static class EntityTypeBuilderExtensions
{
    public static IndexBuilder<TEntity> HasUniqueActiveIndex<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> propertyExpression)
        where TEntity : BaseEntity
    {
        return builder.HasIndex(propertyExpression)
            .IsUnique()
            .HasFilter(
                $"\"{nameof(BaseEntity.EntityStatus)}\" = '{EntityStatus.Active}'");
    }
}
