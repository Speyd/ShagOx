using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Query;
public class ProductTypeQueryService 
    : BaseTranslatableQueryService<
        ProductTypeDto,
        ProductType,
        ProductTypeSearchFilter
        >,
    IProductTypeQueryService
{
    private readonly IProductTypeTranslationQueryRepository _translationRepository;


    public ProductTypeQueryService(
        IProductTypeQueryRepository productTypeQueryRepository,
        IProductTypeTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(productTypeQueryRepository, language, cacheService, settings)
    {
        _translationRepository = translationRepository;
    }


    public override async Task<ProductTypeDto> ApplyMapperAsync(
        ProductType entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return ProductTypeMapper.ToDto(entity, translation?.Name);
    }
}
