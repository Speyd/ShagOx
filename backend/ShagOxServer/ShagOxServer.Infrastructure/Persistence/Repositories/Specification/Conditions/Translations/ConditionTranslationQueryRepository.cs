using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations;
public class ConditionTranslationQueryRepository
    : QueryTranslationRepository<ConditionTranslation, 
        ConditionTranslationSearchFilter>,
      IConditionTranslationQueryRepository
{
    public ConditionTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<ConditionTranslation> ApplyIncludes(
         IQueryable<ConditionTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<ConditionTranslation> ApplyFilter(
       IQueryable<ConditionTranslation> query,
       ConditionTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public override async Task<ConditionTranslation?> GetByIdentificatorAsync(
        string identificator,
        string language)
    {
        return await _db.ConditionTranslations
          .FirstOrDefaultAsync(x =>
            x.Translatable.Code == identificator &&
            x.Language == language);
    }
}