using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
public class AttributeDictionaryValueQueryRepository
    : QueryRepository<AttributeDictionaryValue>,
      IAttributeDictionaryValueQueryRepository
{
    public AttributeDictionaryValueQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<AttributeDictionaryValue>> GetByDictionaryAsync(
        long dictionaryId, 
        PaginationParams pagination)
    {
        return await _db.AttributeDictionaryValues
           .Where(x => x.DictionaryId == dictionaryId)
           .ToPagedResultAsync(pagination);
    }


    public async Task<PagedResult<AttributeDictionaryValue>> Search(
        AttributeDictionaryValueSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.AttributeDictionaryValues
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}