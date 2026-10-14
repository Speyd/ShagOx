using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base.Translations.Translation;
public interface ITranslationQueryOperations<TDto, TFilter>
    : IQueryOperations<TDto, TFilter>
    where TDto : BaseDto
    where TFilter : BaseFilter

{
    Task<Result<PagedResult<TDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language,
        CancellationToken cancellationToken = default);

    Task<Result<TDto>> GetByIdentificatorAsync(
        string identificator,
        string language,
        CancellationToken cancellationToken = default);
}
