using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations;
public class ConditionTranslationExistsRepository
    : ExistsTranslationRepository<Condition,
        ConditionTranslation>,
      IConditionTranslationExistsRepository
{
    public ConditionTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<ConditionTranslation> ApplyIncludes(
         IQueryable<ConditionTranslation> query)
    {
        return query.WithIncludes();
    }
}