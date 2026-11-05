using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions;
public class AttributeDefinitionExistsRepository
    : ExistsRepository<AttributeDefinition>,
      IAttributeDefinitionExistsRepository
{
    public AttributeDefinitionExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByCategoryAsync(
        long attributeId,
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDefinitions
            .WithStatusFilter(status)
            .AnyAsync(x =>
            (x.Id == attributeId &&
            x.CategoryId == categoryId),
            cancellationToken);
    }

    public async Task<bool> ExistsByCategoryAsync(
        string attributeKey,
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDefinitions
            .WithStatusFilter(status)
            .AnyAsync(x =>
            (x.Key == attributeKey &&
            x.CategoryId == categoryId),
            cancellationToken);
    }
}
