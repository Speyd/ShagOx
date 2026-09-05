using ShagOxServer.Application.DTOs.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
public interface ICityTranslationQueryService
    : IQueryTranslationService<CityTranslationDto>
{
    Task<Result<PagedResult<CityTranslationDto>>> Search(
       CityTranslationSearchFilter filter,
       PaginationParams pagination);
}