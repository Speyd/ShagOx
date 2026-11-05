using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Api.Controllers.Dictionaries.ProductTypes.Translations;
[ApiController]
[Route("api/product-types/translations")]
public class ProductTypeTranslationQueriesController 
    : BaseTranslationQueryController<ProductTypeTranslationDto,
        ProductTypeTranslationSearchFilter>
{
    public ProductTypeTranslationQueriesController(
        IProductTypeTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
