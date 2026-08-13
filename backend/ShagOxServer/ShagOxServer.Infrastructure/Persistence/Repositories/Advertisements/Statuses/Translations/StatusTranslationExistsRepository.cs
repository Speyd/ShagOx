using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations;
public class StatusTranslationExistsRepository
 : ExistsRepository<Status>,
      IStatusTranslationExistsRepository
{
    public StatusTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByCodeAsync(
        string name)
    {
        return await _db.StatusTranslations
            .AnyAsync(x => x.Name == name);
    }

    public async Task<bool> ExistsByLanguageAsync(
        string language)
    {
        return await _db.StatusTranslations
            .AnyAsync(x => x.Language == language);
    }
}