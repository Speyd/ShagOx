using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries;
public class AttributeDictionaryQueryRepository
    : QueryRepository<AttributeDictionary>,
      IAttributeDictionaryQueryRepository
{
    public AttributeDictionaryQueryRepository(AppDbContext db)
        : base(db)
    { }


    public override async Task<AttributeDictionary?> GetByIdAsync(
        long id)
    {
        return await _db.AttributeDictionaries
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedResult<AttributeDefinition>> GetDefinitionsAsync(
        long attributeDictionaryId,
        PaginationParams pagination)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .Where(x => x.DictionaryId == attributeDictionaryId)
            .ToPagedResultAsync(pagination);
    }

    public override async Task<PagedResult<AttributeDictionary>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.AttributeDictionaries
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<AttributeDictionary>> Search(
        AttributeDictionarySearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.AttributeDictionaries
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}