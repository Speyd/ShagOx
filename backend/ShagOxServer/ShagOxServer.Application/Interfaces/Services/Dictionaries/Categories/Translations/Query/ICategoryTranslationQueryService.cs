using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
public interface ICategoryTranslationQueryService
    : IQueryTranslationService<CategoryTranslationDto>
{
    Task<Result<PagedResult<CategoryTranslationDto>>> Search(
       CategoryTranslationSearchFilter filter,
       PaginationParams pagination);
}