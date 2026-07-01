using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
public class AttributeDefinitionRepository : BaseRepository, IAttributeDefinitionRepository
{
    public AttributeDefinitionRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<AttributeDefinition?> GetByIdAsync(int id)
    {
        return await _db.AttributeDefinitions.WithIncludes()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(AttributeDefinition attribute)
    {
        await _db.AttributeDefinitions.AddAsync(attribute);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(AttributeDefinition attribute)
    {
        _db.AttributeDefinitions.Update(attribute);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(AttributeDefinition attribute)
    {
        _db.AttributeDefinitions.Remove(attribute);

        await _db.SaveChangesAsync();
    }
}
