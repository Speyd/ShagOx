using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
public class AttributeDefinitionQueryRepository : BaseRepository, IAttributeDefinitionQueryRepository
{
    public AttributeDefinitionQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<AttributeDefinition?> GetByIdAsync(int id)
    {
        return await _db.AttributeDefinitions.WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<AttributeDefinition>> GetByCategoryAsync(int categoryId)
    {
        return await _db.AttributeDefinitions.WithIncludes()
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<AttributeDefinition>> GetByIdsAsync(
        List<int> ids)
    {
        return await _db.AttributeDefinitions.WithIncludes()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<List<AttributeDefinition>> SearchByKey(
        string key,
        int page,
        int pageSize)
    {
        return await _db.AttributeDefinitions.WithIncludes()
            .Where(x => x.Key.Contains(key))
            .ToListAsync();
    }
}
