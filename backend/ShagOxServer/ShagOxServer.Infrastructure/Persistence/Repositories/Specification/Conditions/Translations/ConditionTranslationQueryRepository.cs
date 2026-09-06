using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations;
public class ConditionTranslationQueryRepository
    : QueryTranslationRepository<ConditionTranslation>,
      IConditionTranslationQueryRepository
{
    public ConditionTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<ConditionTranslation>> Search(
        ConditionTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.ConditionTranslations
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}