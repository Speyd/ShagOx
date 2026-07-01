using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;

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

    private IQueryable<AttributeDefinition> Query()
    {
        return _db.AttributeDefinitions
               .Include(x => x.Category);
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.AttributeDefinitions
            .AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByCategoryAsync(int attributeId, int categoryId)
    {
        return await _db.AttributeDefinitions
            .AnyAsync(x => 
            (x.Id == attributeId &&
            x.CategoryId == categoryId));
    }

    public async Task<List<AttributeDefinition>> GetByCategoryAsync(int categoryId)
    {
        return await Query().
            Where(x => x.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<AttributeDefinition?> GetByIdAsync(int id)
    {
        return await Query()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<AttributeDefinition>> GetByIdsAsync(
        List<int> ids)
    {
        return await Query()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}
