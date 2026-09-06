using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations;
public class RegionTranslationExistsRepository
    : ExistsTranslationRepository<RegionTranslation>,
      IRegionTranslationExistsRepository
{
    public RegionTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.RegionTranslations
            .AnyAsync(x => x.Name == name);
    }
}