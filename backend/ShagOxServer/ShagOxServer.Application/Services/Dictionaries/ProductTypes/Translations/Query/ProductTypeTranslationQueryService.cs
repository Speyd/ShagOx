using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Query;
public class ProductTypeTranslationQueryService
    : BaseTranslationQueryService<
        ProductTypeTranslationDto,
        ProductTypeTranslation,
        ProductTypeTranslationSearchFilter
        >,
    IProductTypeTranslationQueryService
{
    public ProductTypeTranslationQueryService(
        IProductTypeTranslationQueryRepository typeRepository
    )
        : base(typeRepository)
    {
    }

    protected override ProductTypeTranslationDto ApplyMapper(
        ProductTypeTranslation entity)
    {
        return ProductTypeTranslationMapper.ToDto(entity);
    }
}