using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
public interface IProductTypeTranslationQueryService
    : IQueryTranslationService<ProductTypeTranslationDto, 
        ProductTypeTranslationSearchFilter>
{
}