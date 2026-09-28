using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
public interface ITranslationQueryRepository<TEntity, TTranslation, TFilter>
    : IQueryRepository<TTranslation, TFilter>
    where TEntity : BaseTranslatable
    where TTranslation : BaseTranslation<TEntity>
    where TFilter : BaseFilter
{
    Task<PagedResult<TTranslation>> GetPagedAsync(
       PaginationParams pagination,
       string language);

    Task<TTranslation?> GetByIdentificatorAsync(
        string identificator,
        string language);
}