using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
public interface IStatusTranslationQueryRepository
    : IQueryRepository<StatusTranslation>
{
    Task<PagedResult<StatusTranslation>> GetPagedAsync(
        PaginationParams pagination,
        string language);

    Task<PagedResult<StatusTranslation>> Search(
       StatusTranslationSearchFilter filter,
       PaginationParams pagination);
}