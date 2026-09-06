using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations;
public class CityTranslationExistsRepository
    : ExistsTranslationRepository<CityTranslation>,
      ICityTranslationExistsRepository
{
    public CityTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.CityTranslations
            .AnyAsync(x => x.Name == name);
    }
}