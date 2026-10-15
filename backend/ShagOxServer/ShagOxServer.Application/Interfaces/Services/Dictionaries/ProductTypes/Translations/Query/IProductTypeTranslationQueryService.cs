using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations.Translation;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
public interface IProductTypeTranslationQueryService
    : ITranslationQueryService<ProductTypeTranslationDto,
        ProductTypeTranslation,
        ProductTypeTranslationSearchFilter>
{
}