using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
public class AttributeDefinitionQueryRepository 
    : RepositoryContext, IAttributeDefinitionQueryRepository
{
    public AttributeDefinitionQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<AttributeDefinition?> GetByIdAsync(int id)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedResult<AttributeDefinition>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<List<AttributeDefinition>> GetByIdsAsync(
        List<int> ids)
    {
        return await _db.AttributeDefinitions.WithIncludes()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<PagedResult<AttributeDefinition>> Search(
        AttributeDefinitionSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}