using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
public interface IQueryTranslationRepository<T>
    : IQueryRepository<T>
{
    Task<PagedResult<T>> GetPagedAsync(
       PaginationParams pagination,
       string language);
}