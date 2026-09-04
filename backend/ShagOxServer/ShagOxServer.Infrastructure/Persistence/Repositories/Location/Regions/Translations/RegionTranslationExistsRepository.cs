using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations;
public class RegionTranslationExistsRepository
    : ExistsRepository<RegionTranslation>,
      IRegionTranslationExistsRepository
{
    public RegionTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        int regionId,
        string language)
    {
        return await _db.RegionTranslations
            .AnyAsync(x => x.RegionId == regionId &&
                x.Language == language);
    }

    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.RegionTranslations
            .AnyAsync(x => x.Name == name);
    }

    public async Task<bool> ExistsByLanguageAsync(
        string language)
    {
        return await _db.RegionTranslations
            .AnyAsync(x => x.Language == language);
    }
}