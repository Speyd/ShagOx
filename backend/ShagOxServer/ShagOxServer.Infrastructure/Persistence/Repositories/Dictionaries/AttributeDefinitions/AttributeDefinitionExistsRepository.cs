using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions;
public class AttributeDefinitionExistsRepository 
    : BaseRepository, IAttributeDefinitionExistsRepository
{
    public AttributeDefinitionExistsRepository(AppDbContext db)
        : base(db)
    { }


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

    public async Task<bool> ExistsByCategoryAsync(string attributeKey, int categoryId)
    {
        return await _db.AttributeDefinitions
            .AnyAsync(x =>
            (x.Key == attributeKey &&
            x.CategoryId == categoryId));
    }
}