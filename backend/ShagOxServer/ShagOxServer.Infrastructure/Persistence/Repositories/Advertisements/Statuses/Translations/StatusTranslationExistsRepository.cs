using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations;
public class StatusTranslationExistsRepository
    : ExistsTranslationRepository<StatusTranslation>,
      IStatusTranslationExistsRepository
{
    public StatusTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.StatusTranslations
            .AnyAsync(x => x.Name == name);
    }
}