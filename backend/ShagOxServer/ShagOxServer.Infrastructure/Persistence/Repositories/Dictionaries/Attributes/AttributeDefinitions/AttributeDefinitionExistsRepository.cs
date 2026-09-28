using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions;
public class AttributeDefinitionExistsRepository
    : ExistsRepository<AttributeDefinition>,
      IAttributeDefinitionExistsRepository
{
    public AttributeDefinitionExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByCategoryAsync(
        long attributeId,
        long categoryId)
    {
        return await _db.AttributeDefinitions
            .AnyAsync(x =>
            (x.Id == attributeId &&
            x.CategoryId == categoryId));
    }

    public async Task<bool> ExistsByCategoryAsync(
        string attributeKey,
        long categoryId)
    {
        return await _db.AttributeDefinitions
            .AnyAsync(x =>
            (x.Key == attributeKey &&
            x.CategoryId == categoryId));
    }
}