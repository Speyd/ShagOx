using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
public interface IProductTypeTranslationQueryService
    : IQueryTranslationService<ProductTypeTranslationDto>
{
    Task<Result<PagedResult<ProductTypeTranslationDto>>> Search(
       ProductTypeTranslationSearchFilter filter,
       PaginationParams pagination);
}