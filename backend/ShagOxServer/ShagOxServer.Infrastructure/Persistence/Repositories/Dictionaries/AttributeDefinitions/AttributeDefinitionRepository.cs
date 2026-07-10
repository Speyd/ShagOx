using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
public class AttributeDefinitionRepository : BaseRepository, IAttributeDefinitionRepository
{
    public AttributeDefinitionRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<AttributeDefinition?> GetByIdAsync(int id)
    {
        return await _db.AttributeDefinitions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Add(AttributeDefinition attribute)
    {
        _db.AttributeDefinitions.Add(attribute);
    }

    public bool Update(AttributeDefinition attribute)
    {
        _db.AttributeDefinitions.Update(attribute);
        return true;
    }

    public void Delete(AttributeDefinition attribute)
    {
        _db.AttributeDefinitions.Remove(attribute);
    }
}