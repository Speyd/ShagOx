using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations;
public class CityTranslationExistsRepository
    : ExistsRepository<CityTranslation>,
      ICityTranslationExistsRepository
{
    public CityTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        int regionId,
        string language)
    {
        return await _db.CityTranslations
            .AnyAsync(x => x.CityId == regionId &&
                x.Language == language);
    }

    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.CityTranslations
            .AnyAsync(x => x.Name == name);
    }

    public async Task<bool> ExistsByLanguageAsync(
        string language)
    {
        return await _db.CityTranslations
            .AnyAsync(x => x.Language == language);
    }
}