using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations;
public class ConditionTranslationExistsRepository
    : ExistsTranslationRepository<ConditionTranslation>,
      IConditionTranslationExistsRepository
{
    public ConditionTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.ConditionTranslations
            .AnyAsync(x => x.Name == name);
    }
}