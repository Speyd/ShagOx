using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries;
public class AttributeDefinitionRepository : BaseRepository, IAttributeDefinitionRepository
{
    public AttributeDefinitionRepository(AppDbContext db)
        : base(db)
    { }

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

   

    public async Task<bool> ExistsByCategoryAsync(int attributeId, int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AttributeDefinition>> GetByCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AttributeDefinition?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
