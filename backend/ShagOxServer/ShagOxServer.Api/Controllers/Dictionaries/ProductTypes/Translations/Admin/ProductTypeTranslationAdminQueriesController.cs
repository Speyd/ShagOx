using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Api.Controllers.Dictionaries.ProductTypes.Translations.Admin;

[ApiController]
[Route("api/admin/product-types/translations")]
[Authorize(Roles = "Admin")]
public class ProductTypeTranslationAdminQueriesController
    : BaseTranslationAdminQueryController<ProductTypeTranslationDto,
        ProductTypeTranslationSearchFilter>
{
    public ProductTypeTranslationAdminQueriesController(
        IProductTypeTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
